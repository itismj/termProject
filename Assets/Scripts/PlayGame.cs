using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class SceneLoader : MonoBehaviour
{
    // Method to load the game scene
    public void LoadGameScene()
    {
        // Replace "GameScene" with the actual name of your game scene
        SceneManager.LoadScene("GameScene");
    }
}
