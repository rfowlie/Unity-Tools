using UnityEngine;

namespace UIEventSystem
{
    public abstract class UIReceiverBase : MonoBehaviour
    {
        protected abstract EReceiver GetReceiverType();
        protected abstract InfoBase[] GetInfo();

        private void Setup()
        {
            foreach (var settings in GetInfo())
            {
                if (settings.subscribe)
                {
                    UIBroadcasterBase broadcaster = UIReceiverHelper.FindBroadcaster(settings.Ebroadcaster, gameObject);
                    foreach (var target in settings.targets)
                    {
                        UIIEffectSetup receiver = UIReceiverHelper.FindReceiver(GetReceiverType(), target);
                        receiver.Setup(broadcaster, settings);
                    }
                }
            }
        }

        private void Start()
        {
            Setup();
        }
    }
}