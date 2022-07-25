using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIEventSystem
{
    public class UIEffectShake : UIEffectTransformBase
    {
        private Vector3 originalPosition;
        private List<ShakeInfo> shakes = new List<ShakeInfo>();

        protected override void Start()
        {
            base.Start();
            originalPosition = rt.position;
        }
        public override void Setup(UIBroadcasterBase broadcaster, object info)
        {
            shakes.Add((ShakeInfo)info);
            broadcaster.BROADCAST += (ctx) => Call((ShakeInfo)info);
        }
        private void Call(ShakeInfo info)
        {
            if (c == null && isActive) { c = StartCoroutine(Effect(info)); }
        }
        protected IEnumerator Effect(ShakeInfo info)
        {
            Debug.Log("Shaking");
            float startRight = Random.Range(0, 100);
            float startUp = Random.Range(0, 100);
            float count = 0f;
            while (count < info.time)
            {
                count += Time.deltaTime;
                rt.position = originalPosition +
                    Vector3.right * info.intensity * Mathf.Sin(startRight + count * info.speed) +
                    Vector3.up * info.intensity * Mathf.Cos(startUp + count * info.speed);
                yield return null;
            }

            rt.position = originalPosition;
            c = null;
        }
    }
}