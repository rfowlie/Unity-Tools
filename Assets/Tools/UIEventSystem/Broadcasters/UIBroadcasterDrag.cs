using UnityEngine.EventSystems;

namespace UIEventSystem
{
    public class UIBroadcasterDrag : UIBroadcasterBase, IDragHandler
    {
        public void OnDrag(PointerEventData eventData)
        {
            CallEvent(eventData);
        }
    }
}