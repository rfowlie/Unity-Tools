using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIEventSystem
{
    public class UIEffectJitter : UIEffectTransformBase
    {
        private float originalRotation;
        private List<JitterInfo> jitters = new List<JitterInfo>();

        protected override void Start()
        {
            base.Start();
            originalRotation = rt.rotation.z;
        }
        public override void Setup(UIBroadcasterBase broadcaster, object info)
        {
            jitters.Add((JitterInfo)info);
            broadcaster.BROADCAST += (ctx) => Call((JitterInfo)info);
        }
        private void Call(JitterInfo info)
        {
            if (c == null && isActive) { c = StartCoroutine(Effect(info)); }
        }
        private IEnumerator Effect(JitterInfo info)
        {
            Debug.Log("Jittering");
            float count = 0f;
            while (count < info.time)
            {
                count += Time.deltaTime;
                rt.rotation = Quaternion.Euler(
                    transform.rotation.x,
                    transform.rotation.y,
                    Mathf.Sin(count * info.rotationSpeed) * (info.rotationAmount - info.rotationAmount * (count / info.time)));
                yield return null;
            }

            rt.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, originalRotation);
            c = null;
        }
    }
}