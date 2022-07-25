using UnityEngine;


namespace UIEventSystem
{
    [System.Serializable]
    public class JitterInfo : InfoBase
    {
        [Range(0, 360)] public float rotationAmount;
        [Positive] public float rotationSpeed;
        [Positive] public float time;
    }
    public class UIReceiverJitter : UIReceiverBase
    {
        [SerializeField] private JitterInfo[] info;
        protected override InfoBase[] GetInfo() { return info; }
        protected override EReceiver GetReceiverType() { return EReceiver.JITTER; }
    }
}


