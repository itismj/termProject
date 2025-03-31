using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        int laneIndex = Random.Range(0, spawnPoints.Length);
        int prefabIndex = Random.Range(0, obstaclePrefabs.Length);

        Vector3 spawnPos = spawnPoints[laneIndex].position;
        Instantiate(obstaclePrefabs[prefabIndex], spawnPos, Quaternion.identity);
    }
}
