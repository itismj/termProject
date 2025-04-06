using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CoinSpawn() {
        float random = Random.Range(-1.8f, 1.8f);
        Instantiate(coinPrefab, new Vector3(random, transform.position.y, transform.position.z), Quaternion.identity);
    }

    IEnumerator SpawnCoins() {
        while (true) {
            int time = Random.Range(1, 5);
            yield return new WaitForSeconds(time);
            CoinSpawn();
        }
    }
}
