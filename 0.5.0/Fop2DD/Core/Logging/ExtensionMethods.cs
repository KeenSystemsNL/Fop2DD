using System;

namespace Fop2DD.Core.Logging
{
    public static class LoggerExtensions
    {
        public static void Log(this IDDLogger logger, DDEventType eventtype, string message)
        {
            logger.Log(new DDLogEntry(eventtype, message, null, null));
        }

        public static void Log(this IDDLogger logger, DDEventType eventtype, string message, params object[] args)
        {
            logger.Log(new DDLogEntry(eventtype, string.Format(message, args), null, null));
        }

        public static void LogDebug(this IDDLogger logger, string message)
        {
            logger.Log(DDEventType.Debug, message);
        }

        public static void LogDebug(this IDDLogger logger, string message, params object[] args)
        {
            logger.Log(DDEventType.Debug, message, args);
        }

        public static void LogInfo(this IDDLogger logger, string message)
        {
            logger.Log(DDEventType.Information, message);
        }

        public static void LogInfo(this IDDLogger logger, string message, params object[] args)
        {
            logger.Log(DDEventType.Information, message, args);
        }

        public static void LogWarning(this IDDLogger logger, string message)
        {
            logger.Log(DDEventType.Warning, message);
        }

        public static void LogWarning(this IDDLogger logger, string message, params object[] args)
        {
            logger.Log(DDEventType.Warning, message, args);
        }

        public static void LogError(this IDDLogger logger, string message)
        {
            logger.Log(DDEventType.Error, message);
        }

        public static void LogError(this IDDLogger logger, string message, params object[] args)
        {
            logger.Log(DDEventType.Warning, message, args);
        }

        public static void LogError(this IDDLogger logger, Exception exception)
        {
            logger.LogException(exception, DDEventType.Error);
        }

        public static void LogFatal(this IDDLogger logger, string message)
        {
            logger.Log(DDEventType.Fatal, message);
        }

        public static void LogFatal(this IDDLogger logger, string message, params object[] args)
        {
            logger.Log(DDEventType.Fatal, message, args);
        }

        public static void LogFatal(this IDDLogger logger, Exception exception)
        {
            logger.LogException(exception, DDEventType.Fatal);
        }

        public static void LogException(this IDDLogger logger, Exception exception, DDEventType eventtype = DDEventType.Error)
        {
            logger.Log(new DDLogEntry(eventtype, exception.Message, null, exception));
        }
    }
}
