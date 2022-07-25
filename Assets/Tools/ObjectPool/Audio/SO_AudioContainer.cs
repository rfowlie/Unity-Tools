using UnityEngine;

[CreateAssetMenu(fileName = "AudioContainer", menuName = "Scriptable Objects/Audio/AudioContainer")]
public class SO_AudioContainer : ScriptableObject
{
    //[SerializeField] public AudioPair[] pairs;
    [SerializeField] public string containerName;
    [SerializeField] public AudioClip[] clips;
}