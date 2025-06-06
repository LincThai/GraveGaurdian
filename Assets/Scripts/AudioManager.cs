using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // set variables
    public Sound[] sounds;

    public static AudioManager Instance;

    private void Awake()
    {
        // singleton to ensure there is only one audiomanager
        if (Instance == null)
        {
            Instance = this;
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
        }
    }

    private void Start()
    {
        // plays background music.
        Play("Background");
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
