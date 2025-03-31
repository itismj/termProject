using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("HIT PLAYER!");
            FindObjectOfType<HealthUI>().Damage(1);
            Destroy(gameObject);
        }
    }
}
