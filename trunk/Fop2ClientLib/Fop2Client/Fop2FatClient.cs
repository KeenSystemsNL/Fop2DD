using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fop2ClientLib
{
    /// <summary>
    /// Represents a FOP2 client (See <a href="http://www.fop2.com/">fop2.com</a>).
    /// </summary>
    /// <remarks>
    /// This class is a descendant of Fop2Client; it extends the Fop2Client with methods abstracting
    /// implementation details on the "Fop2 protocol"
    /// </remarks>
    public class Fop2FatClient : Fop2Client
    {
        /// <summary>
        /// Initializes a new instance of the Fop2FatClient class with the default encoding.
        /// </summary>
        public Fop2FatClient()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the Fop2FatClient class with the specified encoding.
        /// </summary>
        /// <param name="encoding">The encoding to use when sending/receiving messages.</param>
        public Fop2FatClient(Encoding encoding)
            : base(encoding)
        { }

        /// <summary>
        /// Dials a number.
        /// </summary>
        /// <param name="destination">The destination to dial</param>
        public void Dial(string destination)
        {
            this.Dial(this.Id.ToString(), destination);
        }

        /// <summary>
        /// Dials a number from the specified origin.
        /// </summary>
        /// <param name="origin">???</param>
        /// <param name="destination">The destination to dial</param>
        /// <remarks>Unsure (or rather: not figured out yet) how the "origin" in the Dial() method of the fop2.js file works exactly</remarks>
        public void Dial(string origin, string destination)
        {
            //TODO: fixup remarks and document origin parameter.
            //Caution: Dial(string number) depends on this method (passing the current "id" (or myposition or...) as origin).
            this.Send(origin, "dial", destination, this.CurrentHash);
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="destination"></param>
        public void Originate(string destination)
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="destination"></param>
        public void PickupRinging(string destination)
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="destination"></param>
        public void Transfer(string destination)
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="destination"></param>
        public void TransferToExternal(string destination)
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="destination"></param>
        public void TransferToVoicemail(string destination)
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="destination"></param>
        public void Record(string destination)
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="destination"></param>
        public void Whisper(string destination)
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="destination"></param>
        public void Spy(string destination)
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        //TODO: implement other methods
    }
}
