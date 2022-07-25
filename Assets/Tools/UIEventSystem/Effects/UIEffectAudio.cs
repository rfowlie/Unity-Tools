using System.Collections.Generic;
using UnityEngine;


namespace UIEventSystem
{
    public class UIEffectAudio : MonoBehaviour, UIIEffectSetup
    {
        private List<SoundInfo> sounds = new List<SoundInfo>();

        public void Setup(UIBroadcasterBase broadcaster, object info)
        {
            sounds.Add((SoundInfo)info);
            broadcaster.BROADCAST += (ctx) => Call((SoundInfo)info);
        }
        private void Call(SoundInfo info)
        {
            AudioMaster.PlayClip(info.audioContainerName, info.containerIndex, transform.position);
        }
    }
}