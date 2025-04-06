using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    public Renderer meshRenderer;

    public float baseSpeed = 0.5f;       // Initial scroll speed
    public float speedIncreaseFactor = 0.005f; // How much speed increases per distance unit (usually smaller than car speed factor)
    public float maxSpeed = 3f;          // Optional: Maximum scroll speed

    private float currentSpeed;
    // --------------

    void Start()
    {
        // Ensure meshRenderer is assigned
        if (meshRenderer == null) {
            meshRenderer = GetComponent<Renderer>();
            if (meshRenderer == null) {
                Debug.LogError("RoadMovement needs a Renderer component assigned or attached.", gameObject);
                enabled = false; // Disable script if no renderer
            }
        }
    }

    void Update()
    {
        // Calculate current speed based on distance
        if (ScoreManager.Instance != null) // Check if ScoreManager exists
        {
            currentSpeed = baseSpeed + (ScoreManager.Instance.GetCurrentDistance() * speedIncreaseFactor);
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed); // Clamp to max speed
        }
        else
        {
            // Fallback if ScoreManager isn't found yet
            currentSpeed = baseSpeed;
             // Don't spam warning every frame: Debug.LogWarning("RoadMovement could not find ScoreManager instance.");
        }

        // Use the calculated currentSpeed
        meshRenderer.material.mainTextureOffset += new Vector2(0, currentSpeed * Time.deltaTime);
    }
}