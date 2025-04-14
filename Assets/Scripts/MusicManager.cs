using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioSource musicSource;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (musicSource != null)
        {
            Debug.Log("Trying to play music...");
            musicSource.Play(); // Just play the clip manually
        }
    }
    
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
