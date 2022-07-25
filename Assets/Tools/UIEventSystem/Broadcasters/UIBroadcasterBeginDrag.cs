using UnityEngine;
using UnityEngine.EventSystems;

namespace UIEventSystem
{
    public class UIBroadcasterBeginDrag : UIBroadcasterBase, IBeginDragHandler, IDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            CallEvent(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("Dragging");
        }
    }
}