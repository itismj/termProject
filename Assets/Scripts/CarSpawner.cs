using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] car;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCars());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Cars() {
        int random = Random.Range(0, car.Length);
        float randomXPosision = Random.Range(-1.8f, 1.8f);
        Instantiate(car[random], new Vector3(randomXPosision, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 90));
    }
    IEnumerator SpawnCars() {
        while (true) {
            yield return new WaitForSeconds(3);
            Cars();
        }
    }
}
