using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScrollDown : MonoBehaviour
{
    public float scrollSpeed = 2f;

    void Update()
    {
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        
        if (transform.position.y < -6f) // or whatever is off-screen
        {
           Destroy(gameObject);
        }
    }
}
