using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 5f;
    float rotationSpeed = 5f;
    public ScoreManager scoreManager;
    public int maxHealth = 3;
    private int currentHealth;
    public HealthUI healthUI;
    public GameObject gameOverPanel;
    private AudioSource engineSound;
    public SpriteRenderer carRenderer;
    [Header("SFX Clips")]
    public AudioClip coinPickClip;
    public AudioClip damageClip;
    public AudioClip crashClip;
    public AudioSource sfxSource;

    private float defaultEngVolume = 0.25f;  // Usual ones
    private float reducedEngVolume = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        // Load selected car index
        int selectedCarIndex = PlayerPrefs.GetInt("SelectedCar", 0);

        // Load all cars
        CarData[] allCars = Resources.LoadAll<CarData>("CarData");
        
        if (selectedCarIndex >= 0 && selectedCarIndex < allCars.Length)
        {
            CarData selectedCar = allCars[selectedCarIndex];

            int savedHealth = PlayerPrefs.GetInt("CarHealth_" + selectedCarIndex, selectedCar.health);
            maxHealth = savedHealth;
            currentHealth = maxHealth;

            // Set car stats
            speed = selectedCar.speed;
            rotationSpeed = selectedCar.handling;

            // Set sprite
            if (carRenderer != null && selectedCar.previewSprite != null)
            {
                carRenderer.sprite = selectedCar.previewSprite;
            }

            currentHealth = maxHealth;
        } else {
            // Fallback values if index is out of bounds
            maxHealth = 3;
            currentHealth = 3;
            speed = 5f;
            rotationSpeed = 5f;
        }

        healthUI.SetHealth(maxHealth, currentHealth);

        engineSound = GetComponent<AudioSource>();
        engineSound.volume = defaultEngVolume;
        engineSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        clamp();
        
    }
    void Movement() {
    // Move right

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 45), rotationSpeed * Time.deltaTime);
        }

            // Move left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 135), rotationSpeed * Time.deltaTime);
        }
        if (transform.rotation.z != 90) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), 10f * Time.deltaTime);
        }
    }
    void clamp() {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -1.8f, 1.8f);
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Cars")
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Coins")
        {
            Destroy(collision.gameObject);
            scoreManager.AddCoin();
            sfxSource.PlayOneShot(coinPickClip);
        }

    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthUI.UpdateHealth(currentHealth);

        if (currentHealth > 0)
        {
            sfxSource.PlayOneShot(damageClip);
            engineSound.volume = reducedEngVolume;
        }
        
        if (currentHealth <= 0)
        {
            Debug.Log("Player Dead");
            sfxSource.PlayOneShot(crashClip);
            
            engineSound.Stop();
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;

            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.SaveCoinsToTotal();
            }
        }
    }
    public void UpgradeHealth(int amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;
        healthUI.SetHealth(maxHealth, currentHealth);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthUI.UpdateHealth(currentHealth);
    }
}
