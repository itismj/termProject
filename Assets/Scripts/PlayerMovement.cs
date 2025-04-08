using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public Transform transform;
    float speed = 5f;
    float rotationSpeed = 5f;
    public ScoreManager scoreManager;
    public int maxHealth  = 3;
    private int currentHealth;
    public HealthUI healthUI;
    public GameObject gameOverPanel;
    private AudioSource engineSound;
    public AudioClip crashClip;
    private AudioSource crashSource;
    public AudioClip damageClip;
    public AudioClip coinPickClip;

    private float defaultEngVolume = 0.25f;  // Usual ones
    private float reducedEngVolume = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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
            AudioSource.PlayClipAtPoint(coinPickClip, transform.position);
        }

    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthUI.UpdateHealth(currentHealth);

        if (currentHealth > 0)
        {
            AudioSource.PlayClipAtPoint(damageClip, transform.position);  
            engineSound.volume = reducedEngVolume;
        }
        
        if (currentHealth <= 0)
        {
            Debug.Log("Player Dead");
            AudioSource.PlayClipAtPoint(crashClip, transform.position);
            
            engineSound.Stop();
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
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
