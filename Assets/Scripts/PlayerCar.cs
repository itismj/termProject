using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCar : MonoBehaviour
{
    public Transform[] lanes; // Assign 3 lane transforms in the Inspector
    public float laneSwitchSpeed = 10f;

    private int currentLane = 1; // 0 = left, 1 = middle, 2 = right
    private Vector3 targetPosition;
    private Vector2 touchStart;
    void Start()
    {
        targetPosition = lanes[currentLane].position;
    }

    void Update()
    {
        #if UNITY_EDITOR
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && currentLane > 0)
        {
            currentLane--;
            targetPosition = lanes[currentLane].position;
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && currentLane < lanes.Length - 1)
        {
            currentLane++;
            targetPosition = lanes[currentLane].position;
        }
        #endif
        // Smooth movement
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSwitchSpeed * Time.deltaTime);

        // Swipe input (mobile)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                float swipeDelta = touch.position.x - touchStart.x;

                if (Mathf.Abs(swipeDelta) > 50f)
                {
                    if (swipeDelta < 0 && currentLane > 0)
                    {
                        currentLane--;
                        targetPosition = lanes[currentLane].position;
                    }
                    else if (swipeDelta > 0 && currentLane < lanes.Length - 1)
                    {
                        currentLane++;
                        targetPosition = lanes[currentLane].position;
                    }
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered with: " + other.name); // Add this line
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle hit!");
            FindObjectOfType<HealthUI>().Damage(1);
            Destroy(other.gameObject);

            if (HealthUI.CurrentHealth <= 0)
            {
                Debug.Log("GAME OVER!");
                // Later → SceneManager.LoadScene("GameOver")
            }
        }
    }
}
