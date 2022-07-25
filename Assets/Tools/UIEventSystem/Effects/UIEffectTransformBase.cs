using UnityEngine;

namespace UIEventSystem
{
    public abstract class UIEffectTransformBase : MonoBehaviour, IToggelUIEffect, UIIEffectSetup
    {
        public void Toggle(bool b) => isActive = b;
        protected bool isActive = true;
        protected RectTransform rt;
        protected Coroutine c = null;

        public abstract void Setup(UIBroadcasterBase broadcaster, object info);

        protected virtual void Start()
        {
            rt = GetComponent<RectTransform>();
            if (rt == null)
            {
                Debug.LogError($"{gameObject.name} does not have a rectTransform");
                Destroy(this);
            }
        }
    }
}