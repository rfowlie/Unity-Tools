using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace ToolTip
{
    public class Controller : MonoBehaviour
    {
        private static Controller instance;
        public Container toolTip;
        public float delayTime = 0.5f;
       

        private void OnEnable()
        {
            ATrigger.PointerEnter += Show;
            ATrigger.PointerExit += Hide;
        }
        private void OnDisable()
        {
            ATrigger.PointerEnter -= Show;
            ATrigger.PointerExit -= Hide;
        }

        private void Awake()
        {
            //create this instance
            instance = this;

            //hide tooltip
            instance.toolTip.gameObject.SetActive(false);
        }

        //setup and show
        private void Show(string content, string header = "", float size = 1f)
        {
            //set the text in preperation...
            instance.toolTip.SetText(content, header);
            //set size
            instance.toolTip.transform.localScale = Vector3.one * size;

            //begin count for delay
            c = StartCoroutine(Delay(delayTime));
        }

        //stop setup if happening, hide
        private void Hide()
        {
            if (c != null)
            {
                StopCoroutine(c);
                c = null;
            }

            instance.toolTip.gameObject.SetActive(false);
        }

        //delay before showing tooltip
        private static Coroutine c = null;
        private IEnumerator Delay(float delay)
        {
            yield return new WaitForSeconds(delay);            
            instance.toolTip.gameObject.SetActive(true);
            c = StartCoroutine(TempUpdate(() => instance.toolTip.UpdatePosition()));
        }

        //update the position every frame when active
        private IEnumerator TempUpdate(Action func)
        {
            while (true)
            {
                func();
                yield return null;
            }
        }
    }
}

