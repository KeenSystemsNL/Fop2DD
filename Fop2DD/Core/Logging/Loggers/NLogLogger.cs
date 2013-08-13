using NLog;
using System;
using System.Diagnostics;

namespace Fop2DD.Core.Logging.Loggers
{
    public class NLogLogger : IDDLogger
    {
        private Logger _logger;

        public NLogLogger(string name)
        {
            _logger = LogManager.GetLogger(name);
        }

        void IDDLogger.Log(DDLogEntry entry)
        {
            if (entry.EventType == DDEventType.Debug)
                Trace.WriteLine(entry.Message);
            _logger.Log(MapLogEntry(entry));
        }

        private LogEventInfo MapLogEntry(DDLogEntry entry)
        {
            return new LogEventInfo(MapLogLevel(entry.EventType), _logger.Name, entry.Message);
        }

        private static LogLevel MapLogLevel(DDEventType eventtype)
        {
            switch (eventtype)
            {
                case DDEventType.Debug:
                    return LogLevel.Debug;
                case DDEventType.Error:
                    return LogLevel.Error;
                case DDEventType.Fatal:
                    return LogLevel.Fatal;
                case DDEventType.Information:
                    return LogLevel.Info;
                case DDEventType.Warning:
                    return LogLevel.Warn;
            }
            throw new ArgumentException(string.Format("Unable to map eventtype '{0}' to loglevel", eventtype));
        }
    }
}
