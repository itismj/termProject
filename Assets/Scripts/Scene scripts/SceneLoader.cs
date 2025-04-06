using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene"); 
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene"); 
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit pressed");
    }
}
