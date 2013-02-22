using Newtonsoft.Json;

namespace Fop2ClientLib
{
    /// <summary>
    /// Represents a (basic) message received from a FOP2 host
    /// </summary>
    public class Fop2Message
    {
        /// <summary>
        /// Gets/sets the "button".
        /// </summary>
        /// <remarks>Sent as 'btn' over the wire.</remarks>
        [JsonProperty("btn")]
        public string Button { get; set; }

        /// <summary>
        /// Gets/sets the command.
        /// </summary>
        /// <remarks>Sent as 'cmd' over the wire.</remarks>
        [JsonProperty("cmd")]
        public string Command { get; set; }

        /// <summary>
        /// Gets/sets the data.
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// Gets/sets the slot.
        /// </summary>
        [JsonProperty("slot")]
        public string Slot { get; set; }
    }
}
