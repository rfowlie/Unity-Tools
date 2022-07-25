using UnityEngine.EventSystems;

namespace UIEventSystem
{
    public class UIBroadcasterClickUp : UIBroadcasterBase, IPointerUpHandler
    {
        public void OnPointerUp(PointerEventData eventData)
        {
            CallEvent(eventData);
            UnityEngine.Debug.Log("Pointer Up");
        }
    }
}