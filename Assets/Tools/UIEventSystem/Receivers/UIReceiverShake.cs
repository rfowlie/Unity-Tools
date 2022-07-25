using UIEventSystem;
using UnityEngine;


namespace UIEventSystem
{
    [System.Serializable]
    public class ShakeInfo : InfoBase
    {
        [Positive] public float intensity;
        [Positive] public float speed;
        [Positive] public float time;
    }
    public class UIReceiverShake : UIReceiverBase
    {
        [SerializeField] private ShakeInfo[] info;
        protected override InfoBase[] GetInfo() { return info; }
        protected override EReceiver GetReceiverType() { return EReceiver.SHAKE; }
    }
}