//All UIEffect classes must implement this in order to function with setup

namespace UIEventSystem
{
    public interface UIIEffectSetup
    {
        void Setup(UIBroadcasterBase broadcaster, object info);
    }
}