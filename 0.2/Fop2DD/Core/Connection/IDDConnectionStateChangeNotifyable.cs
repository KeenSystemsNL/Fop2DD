using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fop2DD.Core.Connection
{
    public interface IDDConnectionStateChangeNotifyable
    {
        void StateChanged(object sender, DDConnectionStateChangedEventArgs e);
    }
}
