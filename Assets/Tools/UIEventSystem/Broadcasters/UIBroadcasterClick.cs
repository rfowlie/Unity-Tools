using UnityEngine.EventSystems;

namespace UIEventSystem
{
    //For click detection on non-UI GameObjects, ensure a PhysicsRaycaster is attached to the Camera
    //Waits for mouse down & mouse up to occur
    public class UIBroadcasterClick : UIBroadcasterBase, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            CallEvent(eventData);
        }
    }
}