using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the game scene
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        // Load the game scene
        SceneManager.LoadScene(3);
    }
    public void ToMainMenu()
    {
        // Load the game scene
        SceneManager.LoadScene(0);
    }
}