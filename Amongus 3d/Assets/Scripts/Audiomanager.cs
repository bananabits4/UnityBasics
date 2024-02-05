
using UnityEngine;
using UnityEngine.Audio;
using System;
using System.ComponentModel;

public class Audiomanager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sound[] sounds;
    public static Audiomanager instance;

    void Awake()
    {
        if (instance == null)instance = this;else{Destroy(gameObject);return;}
        DontDestroyOnLoad(gameObject);
        foreach (Sound s  in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            if (s.playonawake){s.source.Play();}
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds,sound => sound.name == name);
        if (s == null){Debug.Log("Audio was not found");return;}
        s.source.Play();
    }
}
