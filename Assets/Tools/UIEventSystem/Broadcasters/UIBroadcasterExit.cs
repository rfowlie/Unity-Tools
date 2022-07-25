using UnityEngine.EventSystems;

namespace UIEventSystem
{
    public class UIBroadcasterExit : UIBroadcasterBase, IPointerExitHandler
    {
        public void OnPointerExit(PointerEventData eventData)
        {
            CallEvent(eventData);
        }
    }
}