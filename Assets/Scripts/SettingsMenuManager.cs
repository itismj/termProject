using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenuManager : MonoBehaviour {
    public TMP_Dropdown graphicsDropdown;
    public Toggle sfxToggle;
    public Slider musicSlider;
    public AudioSource musicSource;
    public AudioMixer myMixer;


    void Start() {
        // Initialize all settings
        InitGraphicsDropdown();
        InitSFXToggle();       
        InitMusicSlider(); 

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }


    // GRAPHICS QUALITY
    void InitGraphicsDropdown() {
        
        if (graphicsDropdown == null) return;

        graphicsDropdown.ClearOptions();
        graphicsDropdown.AddOptions(new System.Collections.Generic.List<string>(QualitySettings.names));

        int savedQuality = PlayerPrefs.GetInt("GraphicsQuality", QualitySettings.GetQualityLevel());
        graphicsDropdown.value = savedQuality;
        graphicsDropdown.RefreshShownValue();

        SetGraphicsQuality(savedQuality);
    }

    public void OnGraphicsDropdownChanged(int index) {
        SetGraphicsQuality(index);
    }

    void SetGraphicsQuality(int index) {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("GraphicsQuality", index);
        PlayerPrefs.Save();
        Debug.Log("Graphics set to: " + QualitySettings.names[index]);
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

    void InitSFXToggle() {
        if (sfxToggle == null || myMixer == null) return;

        bool sfxOn = PlayerPrefs.GetInt("SFXEnabled", 1) == 1;
        sfxToggle.isOn = sfxOn;

        // Apply value to the mixer immediately
        myMixer.SetFloat("sfx", sfxOn ? 0f : -80f);
    }

    public void OnSFXToggleChanged(bool isOn) {
        float volume = isOn ? 0f : -80f;
        myMixer.SetFloat("sfx", volume);

        PlayerPrefs.SetInt("SFXEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }


    // MUSIC SLIDER
    void InitMusicSlider() {
        if (musicSlider == null || musicSource == null) return;

        float savedVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        musicSlider.value = savedVol;
        musicSource.volume = savedVol;
    }

    public void OnMusicSliderChanged(float volume) {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();

        if (MusicManager.Instance != null) {
            MusicManager.Instance.SetMusicVolume(volume);
            Debug.Log("Music volume set to: " + volume);
        }
    }
}
