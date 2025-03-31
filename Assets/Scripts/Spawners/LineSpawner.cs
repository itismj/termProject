using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LineSpawner : MonoBehaviour
{
    public GameObject linePrefab;
    public Transform lineParent;
    public Vector2[] spawnPositions = new Vector2[]
    {
        new Vector2(-0.8f, 6f),
        new Vector2(0.8f, 6f)
    };
    private float timer;
    public float spawnInterval = 3.0f;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnLines();
            timer = 0f;
        }
    }

    void SpawnLines()
    {
        foreach (Vector2 pos in spawnPositions)
        {
            Debug.Log("Spawning line at X = " + pos.x); 
            GameObject line = Instantiate(linePrefab, new Vector3(pos.x, pos.y, 0f), Quaternion.identity, lineParent);
            line.transform.localScale = new Vector3(0.05f, 1.0f, 1f);
        }
    }
}
