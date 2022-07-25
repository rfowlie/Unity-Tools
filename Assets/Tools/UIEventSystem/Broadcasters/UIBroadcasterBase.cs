using UnityEngine;
using UnityEngine.EventSystems;

namespace UIEventSystem
{
    public abstract class UIBroadcasterBase : MonoBehaviour
    {
        public event System.Action<PointerEventData> BROADCAST;

        //allows subclasses to call event
        protected void CallEvent(PointerEventData e) { BROADCAST?.Invoke(e); }
    }
}