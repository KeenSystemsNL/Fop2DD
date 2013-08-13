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
            //For now we simply always return an NLogLogger
            //TODO: We should be able to use the configuration file to select which logger to use
            return new NLogLogger(name);
        }
    }
}
