using System.Collections.Generic;
using UnityEngine;


namespace UIEventSystem
{
    [System.Serializable]
    public class SoundInfo : InfoBase
    {
        public string audioContainerName;
        public int containerIndex;
    }
    public class UIReceiverAudio : UIReceiverBase
    {
        public List<SoundInfo> info;
        protected override InfoBase[] GetInfo() { return info.ToArray(); }
        protected override EReceiver GetReceiverType() { return EReceiver.AUDIO; }
    }
}