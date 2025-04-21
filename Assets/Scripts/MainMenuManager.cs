using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    // Start is called before the first frame update
    void Start()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        coinsText.text = "Total Coins: " + totalCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
