
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound 
{
    public string name;
    [Range(0,1)]
    public float volume;
    public AudioClip clip;
    [Range(0.1f,3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;
    public  bool loop;
    public bool playonawake;
}
