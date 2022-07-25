using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIEventSystem
{
    public class UIEffectPulse : UIEffectTransformBase
    {
        private Vector2 originalScale;
        private List<PulseInfo> pulses = new List<PulseInfo>();

        protected override void Start()
        {
            base.Start();
            originalScale = new Vector2(rt.rect.width, rt.rect.height);
        }
        public override void Setup(UIBroadcasterBase broadcaster, object info)
        {
            pulses.Add((PulseInfo)info);
            broadcaster.BROADCAST += (ctx) => Call((PulseInfo)info);
        }
        private void Call(PulseInfo info)
        {
            //add some funky logic here to start the new pulse only if after halfway
            //then start at that point so size change doesn't look odd...
            if (c == null && isActive) { c = StartCoroutine(Effect(info)); }
        }
        private IEnumerator Effect(PulseInfo info)
        {
            Debug.Log("Pulsing");
            float halfTime = info.time / 2f;
            float count = 0f;
            while (count < halfTime)
            {
                rt.sizeDelta = Vector2.Lerp(originalScale, originalScale * info.sizeMultiplier, count / halfTime);
                count += Time.deltaTime;
                yield return null;
            }
            count = 0f;
            while (count < halfTime)
            {
                rt.sizeDelta = Vector2.Lerp(originalScale * info.sizeMultiplier, originalScale, count / halfTime);
                count += Time.deltaTime;
                yield return null;
            }

            rt.sizeDelta = originalScale;
            c = null;
        }
    }
}