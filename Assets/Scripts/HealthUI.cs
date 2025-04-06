using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject heartPrefab; // Assign in Inspector
    private List<Image> hearts = new List<Image>();

    public void SetHealth(int maxHealth, int currentHealth)
    {
        // Clear old hearts
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        hearts.Clear();

        // Create new hearts based on maxHealth
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heartGO = Instantiate(heartPrefab, transform);
            Image heartImage = heartGO.GetComponent<Image>();
            hearts.Add(heartImage);

            // Enable only if under current health
            heartImage.enabled = i < currentHealth;
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].enabled = i < currentHealth;
        }
    }
}
