using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    // set variables
    // name of sound
    public string name;

    // the clip
    public AudioClip clip;

    // the volume with a slider set for a range from 0 to 1
    [Range(0f, 1f)]
    public float volume = 1f;

    // the pitch with a slider set for a range of 0.1 to 3
    [Range(.1f, 3f)]
    public float pitch = 1f;

    // if the sound will loop
    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public AudioMixerGroup mixerGroup;
}
