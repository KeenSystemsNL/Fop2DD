using Fop2DD.Core.Logging.Loggers;
using System;

namespace Fop2DD.Core.Logging
{
    public class DDLogManager
    {
        public static IDDLogger GetLogger(Type type)
        {
            return DDLogManager.GetLogger(type.Name);
        }

        public static IDDLogger GetLogger(string name) {
            //For now we simply always return a NullLogger
            return new DDNullLogger();
        }
    }
}
