using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleByY : MonoBehaviour
{
    public float minY = -5f;      // Bottom of screen
    public float maxY = 5f;       // Top of screen
    public float minScale = 0.02f; // Smallest scale when far away
    public float maxScale = 1.2f; // Largest scale when close


    void Update()
    {
        float t = Mathf.InverseLerp(minY, maxY, transform.position.y);
        float scale = Mathf.Lerp(minScale, maxScale, 1 - t); // invert t so objects grow as they move down
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}
