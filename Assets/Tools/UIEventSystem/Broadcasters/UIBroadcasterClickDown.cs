using UnityEngine.EventSystems;

namespace UIEventSystem
{
    public class UIBroadcasterClickDown : UIBroadcasterBase, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            CallEvent(eventData);
            UnityEngine.Debug.Log("Pointer Down");
        }
    }
}