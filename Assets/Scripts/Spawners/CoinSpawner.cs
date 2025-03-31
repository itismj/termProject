using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform[] lanePositions;
    public float spawnInterval = 1.5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnCoin();
            timer = 0f;
        }
    }

    void SpawnCoin()
    {
        int laneIndex = Random.Range(0, lanePositions.Length);
        Vector3 spawnPos = lanePositions[laneIndex].position;
        Instantiate(coinPrefab, spawnPos, Quaternion.identity);
    }
}
