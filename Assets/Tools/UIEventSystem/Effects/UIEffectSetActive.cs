using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIEventSystem
{
    public class UIEffectSetActive : MonoBehaviour, UIIEffectSetup
    {
        private List<SetActiveInfo> sets = new List<SetActiveInfo>();

        public void Setup(UIBroadcasterBase broadcaster, object info)
        {
            sets.Add((SetActiveInfo)info);
            broadcaster.BROADCAST += (ctx) => Call((SetActiveInfo)info);
        }

        private void Call(SetActiveInfo info)
        {
            if (info.Activate.Length > 0)
            {
                Debug.Log("Activate");
                foreach (GameObject o in info.Activate) { o.SetActive(true); }
            }
            if (info.Deactivate.Length > 0)
            {
                Debug.Log("Deactivate");
                foreach (GameObject o in info.Deactivate) { o.SetActive(false); }
            }
        }
    }
}