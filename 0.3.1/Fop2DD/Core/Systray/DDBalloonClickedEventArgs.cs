using System;

namespace Fop2DD.Core.Systray
{
    public class DDBalloonClickedEventArgs : EventArgs
    {
        public DDBalloonInfo BalloonInfo { get; private set; }

        public DDBalloonClickedEventArgs(DDBalloonInfo ballooninfo)
        {
            this.BalloonInfo = ballooninfo;
        }
    }
}
