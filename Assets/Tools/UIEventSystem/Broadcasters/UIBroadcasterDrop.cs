using UnityEngine.EventSystems;

namespace UIEventSystem
{
    public class UIBroadcasterDrop : UIBroadcasterBase, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            CallEvent(eventData);
        }
    }
}