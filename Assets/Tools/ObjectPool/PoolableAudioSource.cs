using System.Collections;
using UnityEngine;

namespace ObjectPooling
{
    public class PoolableAudioSource : Poolable<PoolableAudioSource>
    {
        [SerializeField] private AudioSource source;
        protected override string SetName() { return "AudioSource"; }

        private void Awake()
        {
            source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
        }

        public void Activate(AudioClip clip)
        {
            if (clip != null)
            {
                source.clip = clip;
                source.Play();
                c = StartCoroutine(AudioFinished());
            }
        }

        private Coroutine c = null;
        IEnumerator AudioFinished()
        {
            yield return new WaitWhile(() => source.isPlaying);
            OnEventComplete(this);
        }
    }
}
