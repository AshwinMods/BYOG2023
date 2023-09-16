using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
   private Vector3 originalPosition;
    private bool isShaking = false;
    private float shakeDuration = 0f;
    private float shakeIntensity = 0f;

    private void Awake()
    {
        originalPosition = transform.position; // Capture the camera's original position at Awake
    }

    private void Update()
    {
        if (isShaking && shakeDuration > 0)
        {
            // Calculate a random offset within the intensity for both X and Y axes
            Vector2 shakeOffset = Random.insideUnitCircle * shakeIntensity;

            // Apply the offset to the camera's position
            transform.position = new Vector3(originalPosition.x + shakeOffset.x, originalPosition.y + shakeOffset.y, originalPosition.z);

            // Reduce the shake duration over time
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            // Reset the camera position when shaking is over or disabled
            shakeDuration = 0f;
            isShaking = false;
            transform.position = originalPosition;
        }
    }

    // Call this function to initiate a camera shake with a specified duration and intensity
    public void ShakeCamera(float duration, float intensity)
    {
        shakeDuration = duration;
        shakeIntensity = intensity;
        isShaking = true;
    }
    
    // Call this function to stop the camera shake
    public void StopShake()
    {
        isShaking = false;
    }
}
