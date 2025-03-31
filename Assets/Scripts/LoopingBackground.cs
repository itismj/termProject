using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float backgroundHeight = 10f;

    void Update()
    {
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        if (transform.position.y <= -backgroundHeight)
        {
            Vector3 pos = transform.position;
            pos.y += backgroundHeight * 2f; 
            transform.position = pos;
        }
    }
}
