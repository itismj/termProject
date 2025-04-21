using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // --- Added ---
    public static ScoreManager Instance { get; private set; } // Static instance
    // -------------

    public int distance = 0;
    public int coins = 0;

    public Text distanceText;
    public Text coinsText;

    
    private void Awake()
    {
        // Singleton pattern: Ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // Optional: Keep the ScoreManager across scene loads if needed
            // DontDestroyOnLoad(gameObject);
        }
    }


    void Start()
    {
        // Reset score on start (important if DontDestroyOnLoad is used)
        distance = 0;
        coins = 0;
        StartCoroutine(Score());
    }

    void Update()
    {
        distanceText.text = $"Distance: {distance}";
        coinsText.text = $"Coins: {coins}";
    }

    public void AddCoin()
    {
        coins++;

    }

    // -Public Accessor for Distance 
    public int GetCurrentDistance()
    {
        return distance;
    }


    IEnumerator Score()
    {
        while (true)
        {
            // Check if game is paused (Time.timeScale == 0) before increasing score
            if (Time.timeScale > 0)
            {
                yield return new WaitForSeconds(0.8f); // Consider if this delay should also decrease slightly
                distance++;
            }
            else
            {
                yield return null; // Wait for the next frame if paused
            }
        }
    }

    public void SaveCoinsToTotal()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += coins;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();

        Debug.Log("Coins Saved: " + totalCoins);
    }
}