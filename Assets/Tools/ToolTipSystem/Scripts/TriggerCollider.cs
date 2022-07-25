namespace ToolTip
{
    public class TriggerCollider : ATrigger
    {
        private void OnMouseEnter()
        {
            CallEnter();
        }

        private void OnMouseExit()
        {
            CallExit();
        }
    }
}