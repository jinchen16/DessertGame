using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound 
{
    public AudioClip clip;
    public AudioSource source;
    public AudioManager.SoundName soundName;
    public bool isLooping;

}
