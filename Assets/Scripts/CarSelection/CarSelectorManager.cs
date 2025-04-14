using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CarSelectorManager : MonoBehaviour
{
    public List<CarData> cars;
    public Image carDisplayImage;
    public TextMeshProUGUI carNameText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI totalCoinsText;
    public TextMeshProUGUI healthUpgradeCostText;  // UI next to the button

    private int[] healthUpgradeCosts = { 5, 15, 25, 35, 45, 55 }; // or generate dynamically

    // public Text speedText;
    // public Text handlingText;

    // public Button upgradeSpeedButton;
    //public Button upgradeHandlingButton;
    public Button upgradeHealthButton;
    public Button playButton;
    public Button leftArrowButton;
    public Button rightArrowButton;

    private int currentCarIndex = 0;

    void Start()
    {
        // RESET COINS TO 1
        // PlayerPrefs.SetInt("TotalCoins", 1);
        // PlayerPrefs.Save();

        // RESET CAR HEALTH TO 3
        // PlayerPrefs.SetInt("CarHealth_" + currentCarIndex, 3); 
        // PlayerPrefs.Save();


        for (int i = 0; i < cars.Count; i++)
        {
            // cars[i].speed = PlayerPrefs.GetInt("CarSpeed_" + i, cars[i].speed);
            // cars[i].handling = PlayerPrefs.GetInt("CarHandling_" + i, cars[i].handling);
            cars[i].health = PlayerPrefs.GetInt("CarHealth_" + i, cars[i].health);
            cars[i].healthUpgradeLevel = PlayerPrefs.GetInt("CarHealthUpgradeLevel_" + i, 0);
        }

        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoinsText.text = "Coins: " + totalCoins;

        leftArrowButton.onClick.AddListener(PrevCar);
        rightArrowButton.onClick.AddListener(NextCar);
        //upgradeSpeedButton.onClick.AddListener(UpgradeSpeed);
        //upgradeHandlingButton.onClick.AddListener(UpgradeHandling);
        playButton.onClick.AddListener(PlayGame);

        UpdateUI();
    }

    public void UpdateUI()
    {
        CarData car = cars[currentCarIndex];

        carDisplayImage.sprite = car.previewSprite;
        carNameText.text = car.carName;
        healthText.text = "Health: " + car.health.ToString();

        int nextHealthLevel = car.healthUpgradeLevel;
        int upgradeCost = Mathf.RoundToInt(5 * Mathf.Pow(1.5f, nextHealthLevel));

        healthUpgradeCostText.text = "Cost: " + upgradeCost;
        
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        upgradeHealthButton.interactable = totalCoins >= upgradeCost;
        

        // speedText.text = "Speed: " + car.speed.ToString();
        // handlingText.text = "Handling: " + car.handling.ToString();
        Debug.Log($"Car: {car.carName}, Health: {car.health}, Upgrade Cost: {upgradeCost}, Total Coins: {totalCoins}");
    }

    public void NextCar() {
        Debug.Log(">>> NEXT button pressed, current index BEFORE = " + currentCarIndex);
        currentCarIndex = (currentCarIndex + 1) % cars.Count;
        Debug.Log(">>> AFTER update, index = " + currentCarIndex);
        UpdateUI();

    }

    public void PrevCar() {
        currentCarIndex = (currentCarIndex - 1 + cars.Count) % cars.Count;
        UpdateUI();
            Debug.Log(">>> PREV button pressed");

    }

    // public void UpgradeSpeed() {
        // cars[currentCarIndex].speed += 1;
        
        // // Save to PlayerPrefs
        // PlayerPrefs.SetInt("CarSpeed_" + currentCarIndex, cars[currentCarIndex].speed);
        // PlayerPrefs.Save();

        // UpdateUI();
    // }

    // public void UpgradeHandling() {
        // cars[currentCarIndex].handling += 1;
        
        // // Save to PlayerPrefs
        // PlayerPrefs.SetInt("CarHandling_" + currentCarIndex, cars[currentCarIndex].handling);
        // PlayerPrefs.Save();

        // UpdateUI();
    // }
    public void UpgradeHealth()
    {
        CarData car = cars[currentCarIndex];

        int currentLevel = car.healthUpgradeLevel;
        int upgradeCost = Mathf.RoundToInt(5 * Mathf.Pow(1.5f, currentLevel)); // Increment by 10 per upgrade

        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        if (totalCoins >= upgradeCost)
        {
            // Deduct coins
            totalCoins -= upgradeCost;
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
            totalCoinsText.text = "Coins: " + totalCoins;
            

            // Upgrade health
            cars[currentCarIndex].health += 1;
            cars[currentCarIndex].healthUpgradeLevel++;

            // Save car health
            PlayerPrefs.SetInt("CarHealth_" + currentCarIndex, cars[currentCarIndex].health);
            PlayerPrefs.SetInt("CarHealthUpgradeLevel_" + currentCarIndex, cars[currentCarIndex].healthUpgradeLevel);
            PlayerPrefs.Save();

            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }


    public void PlayGame() {
        CarData selectedCar = cars[currentCarIndex];

        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
        PlayerPrefs.SetInt("PlayerHealth", selectedCar.health);
        PlayerPrefs.SetInt("PlayerSpeed", selectedCar.speed);
        PlayerPrefs.SetInt("PlayerHandling", selectedCar.handling);
        PlayerPrefs.Save();

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    [ContextMenu("Reset Health Upgrade Cost to 5")]
    public void ResetUpgradeCostToFive()
    {
        int index = currentCarIndex;

        // Reset upgrade level
        PlayerPrefs.SetInt("CarHealthUpgradeLevel_" + index, 0);

        // Optionally reset health to base value (e.g., 3)
        PlayerPrefs.SetInt("CarHealth_" + index, 3);

        PlayerPrefs.Save();

        // Apply reset to in-memory data
        cars[index].healthUpgradeLevel = 0;
        cars[index].health = 3;

        Debug.Log("Reset upgrade cost to 5 for car index: " + index);

        UpdateUI();
    }

    
    [ContextMenu("Reset All Car Healths")]
    public void ResetAllCarHealths()
    {
        for (int i = 0; i < cars.Count; i++)
        {
            PlayerPrefs.DeleteKey("CarHealth_" + i);
            PlayerPrefs.DeleteKey("CarHealthUpgradeLevel_" + i);
        }
        PlayerPrefs.Save();
        Debug.Log("All car health values reset!");
    }

}
