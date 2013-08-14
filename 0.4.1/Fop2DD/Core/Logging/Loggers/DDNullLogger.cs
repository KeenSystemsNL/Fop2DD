
namespace Fop2DD.Core.Logging.Loggers
{
    public class DDNullLogger : IDDLogger
    {
        void IDDLogger.Log(DDLogEntry entry)
        {
            //NOP
        }
    }
}
