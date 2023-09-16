using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;           // The player character's transform to follow
    public float smoothSpeed = 5f;     // The smoothness of the camera movement
    public Vector3 offset = new Vector3(0f, 0f, -10f);  // The offset from the player
   // public CameraShake cameraShake;  // Reference to the CameraShake2D script

  /*  private void LateUpdate()
    {
        if (!cameraShake.IsShaking())  // Check if the camera is not shaking
        {
            if (target == null)
            {
                return; // Make sure there's a valid target to follow
            }

            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    } */
}
