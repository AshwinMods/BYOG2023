using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    public float shakeDuration = 0.2f;
    public float shakeIntensity = 0.2f;
    public AudioSource gameOverSound; // Assign the AudioSource for the game over sound in the Inspector
    public GameObject playerExplosionPrefab; // Assign the player explosion prefab in the Inspector

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player")) // Check if the collided object is the player
        {
            // Shake the camera
            Camera.main.GetComponent<CameraShake>().ShakeCamera(shakeDuration, shakeIntensity);

            // Play the game over sound
            if (gameOverSound != null)
            {
                gameOverSound.Play();
            }

            // Instantiate a player explosion particle effect
            if (playerExplosionPrefab != null)
            {
                Instantiate(playerExplosionPrefab, other.transform.position, Quaternion.identity);
            }

            // Destroy the player object
            Destroy(other.gameObject);

            // Restart the game by reloading the current scene after a delay (optional)
            // You can adjust the delay duration as needed
            float restartDelay = 0.4f;
            Invoke("RestartGame", restartDelay);
        }
    }

    private void RestartGame()
    {
        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}

