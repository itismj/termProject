using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    // --- Modified ---
    public float baseSpeed = 4f;         // Initial speed
    public float speedIncreaseFactor = 0.02f; // How much speed increases per distance unit
    public float maxSpeed = 15f;         // Optional: A maximum speed limit

    private float currentSpeed;
    // -------------

    void Update()
    {
        // Calculate current speed based on distance 
        if (ScoreManager.Instance != null) // Check if ScoreManager exists
        {
            // Speed calculation: base + (distance * factor)
            currentSpeed = baseSpeed + (ScoreManager.Instance.GetCurrentDistance() * speedIncreaseFactor);
            // Clamp speed to the maximum value
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        }
        else
        {
            // Fallback if ScoreManager isn't found yet
            currentSpeed = baseSpeed;
            Debug.LogWarning("CarMovement could not find ScoreManager instance.");
        }
        // ---------------------------------------------------------

        // Use the calculated currentSpeed
        transform.position -= new Vector3(0, currentSpeed * Time.deltaTime, 0);

        // Destroy if off-screen (consider adjusting this threshold if maxSpeed is high)
        if (transform.position.y <= -12) // Slightly increased threshold
        {
            Destroy(gameObject);
        }
    }
}