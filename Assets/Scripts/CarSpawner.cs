using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] car;

    // Dynamic Spawn Rate
    public float baseSpawnDelay = 3f;       // Initial delay between spawns
    public float minSpawnDelay = 0.5f;      // Minimum time between spawns
    public float spawnRateIncreaseFactor = 0.01f; // How much delay decreases per distance unit
    // ------------------------------------

    void Start()
    {
        StartCoroutine(SpawnCars());
    }

    void Cars()
    {
        int random = Random.Range(0, car.Length);
        float randomXPosision = Random.Range(-1.8f, 1.8f);
        // Consider adding an offset if cars spawn *exactly* at spawner Y and immediately move
        Vector3 spawnPos = new Vector3(randomXPosision, transform.position.y, transform.position.z);
        Instantiate(car[random], spawnPos, Quaternion.Euler(0, 0, 90));
    }

    IEnumerator SpawnCars()
    {
        while (true)
        {
            // Calculate current spawn delay
            float currentSpawnDelay = baseSpawnDelay;
            if (ScoreManager.Instance != null)
            {
                // Decrease delay based on distance
                currentSpawnDelay = baseSpawnDelay - (ScoreManager.Instance.GetCurrentDistance() * spawnRateIncreaseFactor);
                // Clamp delay to the minimum value (ensure it doesn't go below min or zero)
                currentSpawnDelay = Mathf.Max(currentSpawnDelay, minSpawnDelay);
            }

            // Wait for the calculated amount of time
            yield return new WaitForSeconds(currentSpawnDelay);

            // Only spawn if the game is not paused
            if (Time.timeScale > 0)
            {
                 Cars();
            }
        }
    }
}