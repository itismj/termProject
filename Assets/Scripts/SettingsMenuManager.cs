using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour {
    public TMP_Dropdown graphicsDropdown;
    public Toggle sfxToggle;
    public Slider musicSlider;
    public AudioSource musicSource;

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
        if (sfxToggle == null) return;

        bool sfxOn = PlayerPrefs.GetInt("SFXEnabled", 1) == 1;
        sfxToggle.isOn = sfxOn;
        AudioListener.volume = sfxOn ? 1 : 0;
    }

    public void OnSFXToggleChanged(bool isOn) {
        PlayerPrefs.SetInt("SFXEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();
        AudioListener.volume = isOn ? 1 : 0;
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
        if (musicSource != null) {
            musicSource.volume = volume;
        }
    }
}
