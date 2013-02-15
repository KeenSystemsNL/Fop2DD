
namespace Fop2DD.Core.Connection
{
    public interface IDDConnectionStateChangeNotifyable
    {
        void StateChanged(object sender, DDConnectionStateChangedEventArgs e);
    }
}
