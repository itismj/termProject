using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject healthIconPrefab; // Drag your Bolt icon prefab here
    public Transform healthBarParent;   // Reference to your HealthBar object
    private List<GameObject> icons = new List<GameObject>();

    private int currentHealth;
    public static int CurrentHealth { get; private set; }

    void Start()
    {
        if (CarSelectionManager.instance == null)
            Debug.LogError("CarSelectionManager is missing in scene!");

        if (CarSelectionManager.instance.selectedCar == null)
            Debug.LogError("Selected car not set!");

        int maxHealth = CarSelectionManager.instance.selectedCar.maxHealth;
        currentHealth = maxHealth;
        CurrentHealth = currentHealth;
        GenerateHealthIcons(maxHealth);
    }

    void GenerateHealthIcons(int amount)
    {
        Debug.Log("Generating health icons: " + amount);
        // Clear existing icons
        foreach (Transform child in healthBarParent)
        {
            Destroy(child.gameObject);
        }
        icons.Clear();

        // Instantiate icons
        for (int i = 0; i < amount; i++)
        {
            GameObject icon = Instantiate(healthIconPrefab, healthBarParent);
            icons.Add(icon);
        }
    }

    public void Damage(int amount)
    {
        currentHealth -= amount;
        CurrentHealth = currentHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, icons.Count);

        for (int i = 0; i < icons.Count; i++)
        {
            icons[i].SetActive(i < currentHealth); // hide icons based on health left
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, icons.Count);

        for (int i = 0; i < icons.Count; i++)
        {
            icons[i].SetActive(i < currentHealth);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(1);
        }
    }
}
