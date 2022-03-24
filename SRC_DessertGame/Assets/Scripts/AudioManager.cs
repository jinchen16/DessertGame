using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour//Script attached to an EmptyObject named AudioManager in which we assign size and files for each Sound 
{
    public enum SoundName {
        gametheme,
        menutheme,
        chopping,
        frying,
        mixing,
        victory,
        gameover,
    }
    private static AudioManager instance;

    public Sound[] sounds;//On Editor is defined the size of this Sound array
    private AudioManager()
    {

    }
    private void Awake()
    {
        if (instance == null) {
            //instance = new AudioManager();//
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        foreach (Sound s in sounds) 
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;//On Editor is assgined the audio file accordingly
            s.isLooping = false;
        }
    }

    public static AudioManager getInsta()
    { return instance; }
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Sound s in sounds)
        {
            if (AudioManager.SoundName.gametheme == s.soundName)
            {
                s.isLooping= true;
                s.source.loop = s.isLooping;
                s.source.clip = Resources.Load<AudioClip>("bensound-cute");
                s.source.volume = 0.15f;
                s.source.Play();
            }
        }
        //getInsta().Play(AudioManager.SoundName.chopping);//test
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play(SoundName soundName) //AudioManager.SoundName.gametheme
    {
        foreach (Sound s in sounds)
        {
            //s.source = gameObject.AddComponent<AudioSource>();
            if (soundName == s.soundName)
                s.source.Play();
        }
    }
    public void Stop(SoundName soundName) //AudioManager.SoundName.gametheme
    {
        foreach (Sound s in sounds)
        {
            //s.source = gameObject.AddComponent<AudioSource>();
            if (soundName == s.soundName)
                s.source.Stop();
        }
    }
}
