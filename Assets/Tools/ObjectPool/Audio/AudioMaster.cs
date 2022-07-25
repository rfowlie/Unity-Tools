using UnityEngine;
using System.Collections.Generic;
using ObjectPooling;


public class AudioMaster : MonoBehaviour
{
    //setup so that currently loaded scenes can only have one of this
    private static AudioMaster instance = null;
    private static Dictionary<string, AudioClip[]> allAudio;
    private static ObjectPool<PoolableAudioSource> pool;

    [SerializeField] private SO_AudioContainer[] audioContainers;
    [Space]
    [SerializeField] private int numberOfSources = 30;
    
    //psuedo singleton
    private void OnEnable()
    {
        if (instance == null) { instance = this; }
        else { Debug.LogError("There are 2 AudioMasters in the scene"); Destroy(this); }
    }

    private void OnDisable()
    {
        instance = null;
    }

    private void Start()
    {
        allAudio = new Dictionary<string, AudioClip[]>();
        for (int i = 0; i < audioContainers.Length; i++)
        {
            if (allAudio.ContainsKey(audioContainers[i].containerName))
            {
                Debug.LogError($"Multiple audio containers with the same name {audioContainers[i].containerName}");
                continue;
            }

            allAudio.Add(audioContainers[i].containerName, audioContainers[i].clips);
        }

        pool = new ObjectPool<PoolableAudioSource>(transform, numberOfSources);
    }

    //play incoming clip
    public static void PlayClip(string containerName, int index, Vector3 worldPosition)
    {        
        if (!allAudio.ContainsKey(containerName)) { Debug.LogError($"AudioMaster does not have a container named {containerName}"); return; }
        if (allAudio[containerName].Length < index) { Debug.LogError($"Container {containerName} does not have index {index}"); return; }

        AudioClip clip = allAudio[containerName][index];
        PoolableAudioSource o = pool.GetObject();
        o.gameObject.transform.localPosition = o.transform.InverseTransformDirection(worldPosition);
        o.gameObject.SetActive(true);
        o.Activate(clip);
    }

    //pass poolable audio source
    public static PoolableAudioSource GetAudioSource()
    {
        return pool.GetObject();
    }
}