using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Timers;
using System.Xml.Linq;

namespace Fop2ClientLib
{
    /// <summary>
    /// Represents a FOP2 client (See <a href="http://www.fop2.com/">fop2.com</a>).
    /// </summary>
    /// <remarks>
    /// This class uses asynchronous communication to communicate to a specified host; messages are 'parsed' and
    /// events are raised when messages are received. The client provides event-based communication to a FOP2 host
    /// and not only raises events for messages but also for (dis)connected changes, authentication success/failure
    /// and other events. It handles most basic messages and, where possible, decodes/deflates data and ensures
    /// messages are correctly escaped/encoded when sent.
    /// </remarks>
    public class Fop2Client : IFop2Client
    {
        #region Events
        /// <summary>
        /// Occurs when the clients connection state changes.
        /// </summary>
        public event ConnectionStateChangedEventHandler ConnectionStateChanged;

        /// <summary>
        /// Occurs when the client has sent a message to the host.
        /// </summary>
        public event MessageSentEventHandler MessageSent;

        /// <summary>
        /// Occurs when the client received a message from the host.
        /// </summary>
        public event MessageReceivedEventHandler MessageReceived;

        /// <summary>
        /// Occurs when the client received and authenticaiton result.
        /// </summary>
        public event AuthenticationResultReceivedEventHandler AuthenticationResultReceived;

        /// <summary>
        /// Occurs at the specified interval (<see cref="HeartbeatInterval"/>).
        /// </summary>
        public event HeartbeatEventHandler Heartbeat;

        /// <summary>
        /// Occurs when the client successfully authenticated.
        /// </summary>
        public event ConnectionErrorEventHandler ConnectionError;
        #endregion

        #region Private variables
        //Internal AsyncClient which we're wrapping in this class.
        private AsyncClient _client;
        //A "buffer" to append received data to
        private StringBuilder _receivebuffer;
        //Settings used when (de)serializing JSON data
        private JsonSerializerSettings _jsonserializersettings;

        //Private "callbacks"/variables to be called/used when authenticating
        private Action _authcallback;
        private Action _authokcallback;
        private bool _checkauthresult;

        //Timer used for heartbeat/keepalive (e.g. ping)
        private Timer _pingtimer;

        //To ensure our URIparser is only registered once (for parsing strings like foo.com:1234 to IP endpoints)
        private static bool _uriparserregistered = false;

        #endregion

        /// <summary>
        /// When sending commands, terminate them with this string
        /// </summary>
        private const string CMDTERMINATOR = "\r\n\r\n";

        /// <summary>
        /// Just here to ensure our stringbuilder has some "sane" initial capacity.
        /// </summary>
        private const int RCVSTRINGBUFFSIZE = 8 * 1024;

        /// <summary>
        /// Default connection timeout in seconds.
        /// </summary>
        private const int DEFAULTCONNECTTIMEOUT = 10;

        #region Public properties
        /// <summary>
        /// Gets whether the client is connected to the host.
        /// </summary>
        public bool IsConnected
        {
            get { return _client.IsConnected; }
        }

        /// <summary>
        /// Gets whether the client is authenticated at the host.
        /// </summary>
        public bool IsAuthenticated
        {
            get { return this.IsConnected && !string.IsNullOrEmpty(this.Username) && !string.IsNullOrEmpty(this.Context); }
        }

        /// <summary>
        /// Gets the "session" key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the host's Fop2 version which is communicated during authentication.
        /// </summary>
        /// <remarks>This value will not be available before/during authentication.</remarks>
        public Version HostVersion { get; private set; }

        /// <summary>
        /// Gets the licenselevel which is communicated during authentication.
        /// </summary>
        /// <remarks>This value will not be available before/during authentication.</remarks>
        public int LicenseLevel { get; private set; }

        /// <summary>
        /// Gets the current context which is set during authentication.
        /// </summary>
        /// <remarks>This value will not be available before/during authentication.</remarks>
        public string Context { get; private set; }

        /// <summary>
        /// Gets the current user which is set during authentication.
        /// </summary>
        /// <remarks>This value will not be available before/during authentication.</remarks>
        public string Username { get; private set; }

        /// <summary>
        /// Gets the current user which is set during authentication.
        /// </summary>
        /// <remarks>This value will not be available before/during authentication.</remarks>
        public string Password { get; private set; }

        /// <summary>
        /// Gets the current MD5 hash for password + key
        /// </summary>
        public string CurrentHash { get; private set; }

        /// <summary>
        /// Gets the "Id" of the useragent (also "position", "button number")
        /// </summary>
        /// <remarks>This value will not be available before/during authentication.</remarks>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the current preferences which are communicated during authentication.
        /// </summary>
        /// <remarks>This value will not be available before/during authentication.</remarks>
        public Dictionary<string, string> Preferences { get; private set; }
        #endregion

        /// <summary>
        /// Gets/sets the interval at which "heartbeat" / "keepalive" messages will be sent (e.g. ping).
        /// </summary>
        public TimeSpan HeartbeatInterval
        {
            get { return TimeSpan.FromMilliseconds(_pingtimer.Interval); }
            set { _pingtimer.Interval = value.TotalMilliseconds; }
        }

        /// <summary>
        /// Initializes a new instance of the Fop2Client class with the default encoding (UTF8).
        /// </summary>
        public Fop2Client()
            : this(Encoding.UTF8) { }

        /// <summary>
        /// Initializes a new instance of the Fop2Client class with the specified encoding.
        /// </summary>
        /// <param name="encoding">The encoding to use when sending/receiving messages.</param>
        public Fop2Client(Encoding encoding)
        {
            //Initialize a heartbeat (ping) timer, default to 20 seconds
            _pingtimer = new Timer(20000);
            _pingtimer.AutoReset = true;
            _pingtimer.Elapsed += (s, ea) => { this.Send("1", "ping", string.Empty, string.Empty); };

            //Initialize a receive buffer
            _receivebuffer = new StringBuilder(RCVSTRINGBUFFSIZE);

            //Set up JSON handling
            _jsonserializersettings = new JsonSerializerSettings();
            _jsonserializersettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            _jsonserializersettings.NullValueHandling = NullValueHandling.Include;

            //Initialize new Async client and subscribe to events
            _client = new AsyncClient(encoding);
            _client.ConnectionStateChanged += (s, e) => { if (this.ConnectionStateChanged != null) this.ConnectionStateChanged(this, e); };
            _client.ConnectionError += (s, e) => { if (this.ConnectionError != null) this.ConnectionError(this, e); };  //Raise ConnectionError event to subscribers
            _client.DataSent += client_DataSent;
            _client.DataReceived += client_DataReceived;
            
            //Register (temporary) URI parser
            //HACK: we're using a GenericUriParser to quickly parse a host:port IPEndpoint (see Connect(hostnameandport) overload) with a pretty uncommon scheme :P
            if (!_uriparserregistered)
            {
                _uriparserregistered = true;
                UriParser.Register(new GenericUriParser(GenericUriParserOptions.NoQuery | GenericUriParserOptions.NoUserInfo | GenericUriParserOptions.NoFragment | GenericUriParserOptions.Idn | GenericUriParserOptions.IriParsing), "fop2clientlibipendpoint", 4445);
            }

            //Initialize state
            this.ResetState();
        }

        /// <summary>
        /// Called when the underlying AsyncClient sent data.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e"><see cref="DataSentEventArgs"/> containing the "raw" data sent.</param>
        private void client_DataSent(object sender, DataSentEventArgs e)
        {
            //Notify subscribers we just sent a message
            if (this.MessageSent != null)
                this.MessageSent(this, new MessageSentEventArgs(e.Data.TrimEnd()));
        }

        /// <summary>
        /// Called when the underlying AsyncClient receives data.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e"><see cref="DataReceivedEventArgs"/> containing the "raw" data received as string.</param>
        private void client_DataReceived(object sender, DataReceivedEventArgs e)
        {
            //Add received data to our "buffer"
            _receivebuffer.Append(e.Data);
            //Have a complete message? (Should be null-terminated string)
            if (e.Data.EndsWith("\0"))
            {
                //Split messages we currently have in our buffer and initialize a new buffer
                var messages = _receivebuffer.ToString().Split(new[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
                _receivebuffer = new StringBuilder(RCVSTRINGBUFFSIZE);  //You could also use _receivebuffer.Clear() but this would force us to .Net 4

                //Process each message
                foreach (var m in messages)
                {
                    var parsedmessage = ProcessMessage(m);

                    //Raise event for each message received
                    if (this.MessageReceived != null)
                        this.MessageReceived(this, new MessageReceivedEventArgs(parsedmessage));
                }
            }
        }

        /// <summary>
        /// Internal method that is called for each (raw) message received. This method will parse the JSON data and handle some 
        /// authentication logic, "normalizing" (e.g. decoding/deflating) message data etc.
        /// </summary>
        /// <param name="rawmessage">The raw string as received.</param>
        /// <returns>Returns a parsed (or "decoded") <see cref="Fop2Message"/>.</returns>
        private Fop2Message ProcessMessage(string rawmessage)
        {
            //Decode JSON data to a Fop2 message
            var parsedmessage = JsonConvert.DeserializeObject<Fop2Message>(rawmessage, _jsonserializersettings);

            //Do we have to check an authentication-result?
            if (_checkauthresult)
                CheckAuthResult(parsedmessage);

            switch (parsedmessage.Command.ToLowerInvariant())
            {
                case "key":
                    //When we receive a key we store it and check if there's an auth callback to be called
                    this.Key = parsedmessage.Data;
                    if (_authcallback != null)
                    {
                        _authcallback.Invoke();
                        _authcallback = null;
                    }
                    break;
                case "preferences":
                    //When we receive preferences we decode the JSON data and store it in our preferences dictionary
                    parsedmessage.Data = this.DecodeBase64(parsedmessage.Data);
                    this.Preferences = JsonConvert.DeserializeObject<Dictionary<string, string>>(parsedmessage.Data);
                    break;
                case "version":
                    //When we receive a version we try to parse it and store it (and some more...) in our Hostversion/Licenselevel etc.
                    var d = parsedmessage.Data.Split(new char[] { '!' });
                    if (d.Length >= 2)
                    {
                        this.HostVersion = new Version(d[0]);
                        //TODO: d[1] should be "currentlicense"; no idea what it means or would/should contain (bool? version? string? int?)
                        this.LicenseLevel = int.Parse(d[2]);
                    }
                    //The following message received will be our authentication result, so we'll want to check it
                    _checkauthresult = true;
                    break;
                case "permit":
                case "permitbtn":
                case "chat":
                case "note":
                case "details":
                case "members":
                case "queuemembers":
                case "restrictq":
                case "xpresence":
                case "setvar":
                case "handshake":
                case "clidname":
                case "clidnum":
                    //These messages (...and then some) contain base64-encoded data
                    parsedmessage.Data = this.DecodeBase64(parsedmessage.Data);
                    break;
                case "zbuttons":
                    //The zbuttons message contains gzipped data... *sigh*
                    parsedmessage.Data = this.Deflate(parsedmessage.Data);

                    //HACK: See ExtractCurrentId method
                    this.Id = ExtractCurrentId(parsedmessage);

                    break;
                case "pong":
                    //When we receive a pong we raise a heartbeat event (with decoded data)
                    if (this.Heartbeat != null)
                        this.Heartbeat(this, new HeartbeatEventArgs(this.DecodeBase64(parsedmessage.Data)));

                    break;
            }
            //Return the 'decoded' message
            return parsedmessage;
        }

        /// <summary>
        /// Extracts the current ID (or "myposition", "position", "button id") from a zbuttons message
        /// </summary>
        /// <param name="message">The message to extract</param>
        /// <remarks>
        /// This was straight lifted from fop2.js and then refactored/rewritten to C#. The exact inner working is not entirely clear
        /// hence the somewhat "unclear" variablenames. Feel free to refactor away; removing this function entirely would be preferred.
        /// </remarks>
        private int ExtractCurrentId(Fop2Message message)
        {
            var lines = message.Data.Split('\n');
            for (var x = 0; x < lines.Length; x++)
            {
                var linedata = lines[x].Split('!');
                var numberparts = linedata[0].Split('@');
                for (var s = 0; s < linedata.Length; s++)
                {
                    var parts = linedata[s].Split(new[] { '=' }, 2);
                    if ((parts[0] != string.Empty) && (parts.Length >= 2) && (parts[0].Equals("EXTENSION", StringComparison.OrdinalIgnoreCase)) && (parts[1].Equals(this.Username, StringComparison.OrdinalIgnoreCase)))
                    {
                        int id;
                        if (int.TryParse(numberparts[0], out id))
                            return id;
                    }
                }
            }
            //We should have an "Id" (position) by now and exited
            throw new Exception("Error parsing Id (position) from zbuttons response");

            #region Original code (for reference)
            //var lineas = textReader.readToEnd().split("\n");
            //for (var x = 0; x < lineas.length; x++) {
            //    var pepe = lineas[x].split("!");
            //    var nritoPartes = pepe[0].split("@");
            //    var nrito = nritoPartes[0];
            //    for (var s = 0; s < pepe.length; s++) {
            //        var partos = pepe[s].split("=", 2);
            //        if (partos[0] !== "") {
            //          ...
            //                botonitos[nrito][partos[0]] = partos[1];
            //                if (partos[0] == "EXTENSION") {
            //                    extenpos[partos[1]] = nrito;
            //                    if (partos[1] == $('myextension').value) {
            //                        myposition = nrito
            //                    }
            //                }
            //        }
            //    }
            //}
            #endregion
        }

        /// <summary>
        /// Internal method to check is an "auth" message returned an "incorrect" (e.g. authentication failed) message or otherwise.
        /// </summary>
        /// <param name="parsedmessage">The message to determine the auth result from.</param>
        private void CheckAuthResult(Fop2Message parsedmessage)
        {
            AuthenticationStatus status = AuthenticationStatus.Failed;
 
            //We're checking already, do not check again
            _checkauthresult = false;
            //Check the actual result
            if (parsedmessage.Command.Equals("incorrect", StringComparison.OrdinalIgnoreCase))
            {
                //Nope, authentication failed
                this.ResetState();

                
                if (this.AuthenticationResultReceived != null)
                    this.AuthenticationResultReceived(this, new AuthenticationResultReceivedEventArgs(AuthenticationStatus.Failed));
            }
            else
            {
                //Authentication is OK, invoke our authok-callback
                if (_authokcallback != null)
                {
                    _authokcallback.Invoke();
                    _authokcallback = null;
                }

                //Start heartbeat
                _pingtimer.Enabled = true;

                status = AuthenticationStatus.Success;
            }

            //Notify subscribers about the authentication status
            if (this.AuthenticationResultReceived != null)
                this.AuthenticationResultReceived(this, new AuthenticationResultReceivedEventArgs(status));

        }

        /// <summary>
        /// Connects to the specified host and port.
        /// </summary>
        /// <param name="hostnameandport">The host (Ip or hostname) and port to connect to.</param>
        /// <remarks>User hostname:port notation for this method.</remarks>
        public void Connect(string hostnameandport)
        {
            var u = new Uri(string.Format("fop2clientlibipendpoint://{0}", hostnameandport));
            this.Connect(u.Host, u.Port, TimeSpan.FromSeconds(DEFAULTCONNECTTIMEOUT));
        }

        /// <summary>
        /// Connects to the specified host.
        /// </summary>
        /// <param name="host">The host (Ip or hostname) to connect to.</param>
        /// <param name="port">The port to connect to.</param>
        public void Connect(string host, int port)
        {
            this.Connect(new IPEndPoint(Dns.GetHostAddresses(host).First(), port), TimeSpan.FromSeconds(DEFAULTCONNECTTIMEOUT));
        }

        /// <summary>
        /// Connects to the specified host and port.
        /// </summary>
        /// <param name="hostnameandport">The host (Ip or hostname) and port to connect to.</param>
        /// <param name="timeout">Specifies the timespan after wich a timeout exception will be thrown during connecting to the other host.</param>
        /// <remarks>User hostname:port notation for this method.</remarks>
        public void Connect(string hostnameandport, TimeSpan timeout)
        {
            var u = new Uri(string.Format("fop2clientlibipendpoint://{0}", hostnameandport));
            this.Connect(u.Host, u.Port, timeout);
        }

        /// <summary>
        /// Connects to the specified host.
        /// </summary>
        /// <param name="host">The host (Ip or hostname) to connect to.</param>
        /// <param name="port">The port to connect to.</param>
        /// <param name="timeout">Specifies the timespan after wich a timeout exception will be thrown during connecting to the other host.</param>
        public void Connect(string host, int port, TimeSpan timeout)
        {
            this.Connect(new IPEndPoint(Dns.GetHostAddresses(host).First(), port), timeout);
        }

        /// <summary>
        /// Connects to the specified host.
        /// </summary>
        /// <param name="ipendpoint">The IpEndpoint to connect to.</param>
        /// <exception cref="ArgumentNullException">Thrown when ipendpoint is null.</exception>
        public void Connect(IPEndPoint ipendpoint)
        {
            this.Connect(ipendpoint, TimeSpan.FromSeconds(DEFAULTCONNECTTIMEOUT));
        }

        /// <summary>
        /// Connects to the specified host.
        /// </summary>
        /// <param name="ipendpoint">The IpEndpoint to connect to.</param>
        /// <param name="timeout">Specifies the timespan after wich a timeout exception will be thrown during connecting to the other host.</param>
        /// <exception cref="ArgumentNullException">Thrown when ipendpoint is null.</exception>
        public void Connect(IPEndPoint ipendpoint, TimeSpan timeout)
        {
            if (ipendpoint == null)
                throw new ArgumentNullException("ipendpoint");

            _client.Connect(ipendpoint, timeout);
        }

        /// <summary>
        /// Disconnects from host.
        /// </summary>
        /// <remarks>All session state (<see cref="Context"/>, <see cref="Username"/> etc.) will be reset.</remarks>
        public void Disconnect()
        {
            _client.Disconnect();
        }

        /// <summary>
        /// Sends a message to the host; the message is wrapped in an "msg" Xml-element's "data" attribute.
        /// </summary>
        /// <param name="args">The parts of the message to send.</param>
        /// <remarks>All arguments are concatenated using a pipe-separator.</remarks>
        /// <exception cref="ArgumentNullException">Thrown when args is null.</exception>
        public void Send(params string[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");

            //Parameters are pipe-separated
            this.Send(string.Join("|", args));
        }

        /// <summary>
        /// Sends a message to the host; the message is wrapped in an "msg" Xml-element's "data" attribute.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <exception cref="ArgumentNullException">Thrown when message is null.</exception>
        public void Send(string message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            //Create XML element with data attribute <msg data="..." />
            XElement msg = new XElement("msg", new XAttribute("data", message));
            //Send message + command-terminator
            _client.Send(string.Format("{0}{1}", msg.ToString(), CMDTERMINATOR));
        }

        /// <summary>
        /// Authenticates for a session.
        /// </summary>
        /// <param name="context">The context to use for the session.</param>
        /// <param name="username">The username to use for the session.</param>
        /// <param name="password">The password to use when authenticating.</param>
        /// <exception cref="ArgumentNullException">Thrown when context, username and/or password is null.</exception>
        public void Authenticate(string context, string username, string password)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (username == null)
                throw new ArgumentNullException("username");
            if (password == null)
                throw new ArgumentNullException("password");

            //Make sure our state is "clean"
            this.ResetState();

            //Context is stored/used in UPPER case
            context = context.ToUpperInvariant();

            //Set up callbacks
            _authcallback = () =>
            {
                this.Send("1", "auth", username, this.GetMd5Hash(password + this.Key));
            };
            _authokcallback = () =>
            {
                this.Context = context;
                this.Username = username;
                this.Password = password;
                this.CurrentHash = this.GetMd5Hash(this.Password + this.Key);
            };

            //Send message to initiate authentication
            if (!string.IsNullOrEmpty(context))
                this.Send(context, "contexto", "1");
            else
                this.Send("GENERAL", "contexto", "0");
        }

        /// <summary>
        /// Returns an MD5 hash for a string in hexadecimal representation.
        /// </summary>
        /// <param name="value">String to be hashed.</param>
        /// <returns>Returns an MD5 hash for a string in hexadecimal representation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
        public string GetMd5Hash(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            var data = MD5.Create().ComputeHash(_client.Encoding.GetBytes(value));
            var sb = new StringBuilder(32);
            for (int i = 0; i < data.Length; i++)
                sb.Append(data[i].ToString("x2"));
            return sb.ToString();
        }

        /// <summary>
        /// Decodes a Base64-encoded string (using the clients specified encoding).
        /// </summary>
        /// <param name="value">The string to Base64decode.</param>
        /// <returns>Returns the decoded string from the Base64 data.</returns>
        /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
        public string DecodeBase64(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            return _client.Encoding.GetString(Convert.FromBase64String(value));
        }

        /// <summary>
        /// Deflates a Base64-encoded string containing gzipped data to a string.
        /// </summary>
        /// <param name="value">The string to decode and deflate.</param>
        /// <returns>Returns a string deflated and decoded from the value argument passed in.</returns>
        /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
        public string Deflate(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            using (MemoryStream inputStream = new MemoryStream(Convert.FromBase64String(value)))
            using (DeflateStream gzip = new DeflateStream(inputStream, CompressionMode.Decompress))
            using (StreamReader reader = new StreamReader(gzip, _client.Encoding))
                return reader.ReadToEnd();
        }

        /// <summary>
        /// Resets "session"-related state of our object.
        /// </summary>
        private void ResetState()
        {
            this.Id = 0;
            this.Key = null;
            this.Context = null;
            this.Username = null;
            this.LicenseLevel = 0;
            this.HostVersion = null;
            this.Preferences = new Dictionary<string, string>();
        }
    }
}
