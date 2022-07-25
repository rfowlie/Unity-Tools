using UnityEngine.EventSystems;

namespace UIEventSystem
{
    public class UIBroadcasterEnter : UIBroadcasterBase, IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            CallEvent(eventData);
        }
    }
}