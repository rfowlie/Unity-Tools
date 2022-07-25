using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace ToolTip
{
    public class TriggerUI : ATrigger, IPointerEnterHandler, IPointerExitHandler
    {
        //use event system to trigger
        public void OnPointerEnter(PointerEventData eventData)
        {
            CallEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CallExit();
        }
    }
}

