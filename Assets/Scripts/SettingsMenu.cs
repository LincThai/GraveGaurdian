using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    // set variables
    // references
    public AudioMixer audioMixer;

    // adjust Master audiomixer group by main volume slider
    public void SetMainVolume(float volume)
    {
        audioMixer.SetFloat("MainVolume", volume);
        PlayerPrefs.SetFloat("mainVolume", volume);
    }

    // adjust BGM audiomixer group by bgm volume slider
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume", volume);
        PlayerPrefs.SetFloat("bgmVolume", volume);
    }

    // adjust SFX audiomixer group by sfx volume slider
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
}
