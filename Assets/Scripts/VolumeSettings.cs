using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // MUSIC
        if (PlayerPrefs.HasKey("MusicVolume"))
            LoadMusicVolume();
        else
            SetMusicVolume();

        // SFX
        if (PlayerPrefs.HasKey("SFXVolume"))
            LoadSFXVolume();
        else
            SetSFXVolume();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;

        if (volume <= 0.0001f) volume = 0.0001f;

        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    private void LoadMusicVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        musicSlider.value = savedVolume;
        SetMusicVolume();
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;

        if (volume <= 0.0001f) volume = 0.0001f;

        myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadSFXVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        sfxSlider.value = savedVolume;
        SetSFXVolume();
    }
}
