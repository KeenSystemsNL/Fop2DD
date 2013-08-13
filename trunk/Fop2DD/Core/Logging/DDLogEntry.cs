using System;
using System.Globalization;

namespace Fop2DD.Core.Logging
{
    public class DDLogEntry
    {
        public DDEventType EventType { get; private set; }
        public string Message { get; private set; }
        public string Source { get; private set; }
        public Exception Exception { get; private set; }
        public DateTime DateTime { get; private set; }

        public DDLogEntry(DDEventType eventtype, string message, string source, Exception exception)
        {
            this.EventType = eventtype;
            this.Message = message;
            this.Source = source;
            this.Exception = exception;
            this.DateTime = DateTime.UtcNow;
        }

        public override string ToString()
        {
            if (this.Exception != null)
                return string.Format(CultureInfo.InvariantCulture,
                    "Date\t\t: {0}\nSeverity\t: {1}\nMessage\t\t: {2}\nSource\t\t: {3}\nException\t: {4}\n",
                    this.DateTime.ToString("O"), this.EventType, this.Message, this.Source ?? "Unknown", this.Exception);

            return string.Format(CultureInfo.InvariantCulture,
                "Date\t\t: {0}\nSeverity\t: {1}\nMessage\t\t: {2}\nSource\t\t: {3}\n",
                this.DateTime.ToString("O"), this.EventType, this.Message, this.Source ?? "Unknown");

        }
    }
}
