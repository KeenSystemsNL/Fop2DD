
namespace Fop2DD.Core.Systray
{
    public class DDBalloonInfo
    {
        public string CallerIdName { get; private set; }
        public string CallerIdNumber { get; private set; }

        public DDBalloonInfo(string calleridname, string calleridnumber)
        {
            this.CallerIdName = calleridname;
            this.CallerIdNumber = calleridnumber;
        }
    }

}
