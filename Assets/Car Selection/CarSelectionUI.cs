using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelectionUI : MonoBehaviour
{
    public CarData[] carOptions;               // Drag in Red & Truck car assets
    public Image carImageDisplay;              // Assign CarImage
    public Text carNameText;                   // Assign CarNameText
    private int currentIndex = 0;

    void Start()
    {
        UpdateUI();
    }

    public void NextCar()
    {
        currentIndex++;
        if (currentIndex >= carOptions.Length) currentIndex = 0;
        UpdateUI();
    }

    public void PreviousCar()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = carOptions.Length - 1;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (carOptions.Length == 0)
        {
            Debug.LogError("carOptions is empty! Assign CarData assets in the Inspector.");
            return;
        }
        
        CarData car = carOptions[currentIndex];
        carImageDisplay.sprite = car.carSprite;
        carNameText.text = car.carName;
        CarSelectionManager.instance.selectedCar = car; // Update selection
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main_Gameplay");
    }
}
