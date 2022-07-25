using System.Collections.Generic;
using UnityEngine;

namespace UIEventSystem
{
    public static class UIReceiverHelper
    {
        //Broadcaster
        public static UIBroadcasterBase FindBroadcaster(EBroadcaster broadcaster, GameObject target) { return broadcasterSetupDict[broadcaster](target); }

        private static Dictionary<EBroadcaster, System.Func<GameObject, UIBroadcasterBase>> broadcasterSetupDict = new Dictionary<EBroadcaster, System.Func<GameObject, UIBroadcasterBase>>()
        {
            { EBroadcaster.CLICK, (g) => GetBroadcaster<UIBroadcasterClick>(g) },
            { EBroadcaster.CLICKDOWN, (g) => GetBroadcaster<UIBroadcasterClickDown>(g) },
            { EBroadcaster.CLICKUP, (g) => GetBroadcaster<UIBroadcasterClickUp>(g) },
            { EBroadcaster.ENTER, (g) => GetBroadcaster<UIBroadcasterEnter>(g) },
            { EBroadcaster.EXIT, (g) => GetBroadcaster<UIBroadcasterExit>(g) },
        };

        private static T GetBroadcaster<T>(GameObject target) where T : UIBroadcasterBase
        {
            T broadcaster = target.GetComponent<T>();
            if (broadcaster == null) { broadcaster = target.AddComponent<T>(); }
            return broadcaster;
        }


        //Receiver
        public static UIIEffectSetup FindReceiver(EReceiver receiver, GameObject target) { return receiverSetupDict[receiver](target); }

        private static Dictionary<EReceiver, System.Func<GameObject, UIIEffectSetup>> receiverSetupDict = new Dictionary<EReceiver, System.Func<GameObject, UIIEffectSetup>>()
        {
            { EReceiver.AUDIO, (g) => GetReceiver<UIEffectAudio>(g) },
            { EReceiver.PULSE, (g) => GetReceiver<UIEffectPulse>(g) },
            { EReceiver.SHAKE, (g) => GetReceiver<UIEffectShake>(g) },
            { EReceiver.JITTER, (g) => GetReceiver<UIEffectJitter>(g) },
            { EReceiver.SETACTIVE, (g) => GetReceiver<UIEffectSetActive>(g) },
        };

        private static T GetReceiver<T>(GameObject target) where T : MonoBehaviour, UIIEffectSetup
        {
            T receiver = target.GetComponent<T>();
            if (receiver == null) { receiver = target.AddComponent<T>(); }
            return receiver;
        }
    }
}