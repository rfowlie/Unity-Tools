using UnityEngine;

namespace UIEventSystem
{
    [System.Serializable]
    public class PulseInfo : InfoBase
    {
        [Positive] public float sizeMultiplier;
        [Positive] public float time;
    }
    public class UIReceiverPulse : UIReceiverBase
    {
        [SerializeField] private PulseInfo[] info;
        protected override InfoBase[] GetInfo() { return info; }
        protected override EReceiver GetReceiverType() { return EReceiver.PULSE; }
    }
}
