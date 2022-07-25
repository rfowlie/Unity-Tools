using System;
using UnityEngine;

namespace ToolTip
{
    public abstract class ATrigger : MonoBehaviour
    {
        //text to be set on this object
        public string header;
        [Multiline()]
        public string content;
        //who big you want the tooltip displayed for this object
        public float size = 1f;

        public static event Action<string, string, float> PointerEnter;
        public static event Action PointerExit;

        //need these so inheriting classes can call the events
        protected void CallEnter() { PointerEnter?.Invoke(content, header, size); }
        protected void CallExit() { PointerExit?.Invoke(); }
    }
}