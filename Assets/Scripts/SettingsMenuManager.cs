using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsMenuManager : MonoBehaviour {
    public TMP_Dropdown graphicsDropdown;
 

    void Start() {
        if (graphicsDropdown == null) return;

        // Load the saved graphics quality (if exists)
        int savedQuality = PlayerPrefs.GetInt("GraphicsQuality", -1);

        // If we have a saved quality, set it, otherwise, use default
        if (savedQuality != -1) {
            graphicsDropdown.value = savedQuality;
        } else {
            graphicsDropdown.value = QualitySettings.GetQualityLevel();
        }
        
        // Set the graphics dropdown options
        graphicsDropdown.ClearOptions();
        graphicsDropdown.AddOptions(new System.Collections.Generic.List<string>(QualitySettings.names));
        graphicsDropdown.RefreshShownValue();

        // Run function when dropdown changes
        graphicsDropdown.onValueChanged.AddListener(ChangeGraphicsQuality);

        // Limit FPS to avoid computer explosion
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }

    // Save the selected quality to PlayerPrefs
    void ChangeGraphicsQuality(int levelIndex) {
        QualitySettings.SetQualityLevel(levelIndex);
        PlayerPrefs.SetInt("GraphicsQuality", levelIndex); // Save it
        PlayerPrefs.Save(); // Make sure the data is saved
        Debug.Log("Graphics quality set to: " + QualitySettings.names[levelIndex]);
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
