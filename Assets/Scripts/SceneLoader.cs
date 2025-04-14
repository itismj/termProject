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

    public void OpenSelectCarPanel()
    {
        SceneManager.LoadScene("CarSelectionScene"); 
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene"); 
    }

    public void OpenInformationPanel()
    {
        SceneManager.LoadScene("InformationScene"); 
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit pressed");
    }
}
