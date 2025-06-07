using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // set variables
    public Sound[] sounds;
    public AudioMixer mainMixer;

    public static AudioManager instance;

    private void Awake()
    {
        // singleton to ensure there is only one audiomanager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            // Assign source with the audiosource that was added 
            s.source = gameObject.AddComponent<AudioSource>();
            // Assign the clip in the source to the clip in the sound
            s.source.clip = s.clip;

            // Assign Pitch and Volume
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            // assign whether the sound will loop
            s.source.loop = s.loop;

            // assign mixer groups
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
    }

    private void Start()
    {
        // set audioMixer Values from player prefs saved in settings
        mainMixer.SetFloat("MainVolume", PlayerPrefs.GetFloat("mainVolume", 0));
        mainMixer.SetFloat("BGMVolume", PlayerPrefs.GetFloat("bgmVolume", 0));
        mainMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("sfxVolume", 0));

        // plays background music.
        Play("BGM");
    }

    public void Play(string name)
    {
        // go through the array and find a sound with the same name
        // as the one given and store in the variable
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // makes sure that there is a sound
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        // play the sound
        s.source.Play();
    }
}
