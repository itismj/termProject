using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public float speed = 0.3f;
    public float resetY = -10f;   // when it goes off-screen
    public float startY = 10f;    // where it resets to (stacked above)


    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y <= resetY)
        {
            Vector3 pos = transform.position;
            pos.y = startY;
            transform.position = pos;
        }
    }
}
