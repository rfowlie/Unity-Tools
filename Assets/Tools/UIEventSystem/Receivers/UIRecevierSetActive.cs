using UIEventSystem;
using UnityEngine;

namespace UIEventSystem
{
    [System.Serializable]
    public class SetActiveInfo : InfoBase
    {
        public GameObject[] Activate;
        public GameObject[] Deactivate;
    }

    public class UIRecevierSetActive : UIReceiverBase
    {
        [SerializeField] private SetActiveInfo[] info;
        protected override InfoBase[] GetInfo() { return info; }
        protected override EReceiver GetReceiverType() { return EReceiver.SETACTIVE; }
    }
}