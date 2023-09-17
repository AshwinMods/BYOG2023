using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Gate : MonoBehaviour
{
    public AudioSource enterDoor;
    public GameObject EnterPrefab;
    public Animator transition;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            if (enterDoor != null)
            {
                enterDoor.Play();
            }
             if (EnterPrefab != null)
            {
                Instantiate(EnterPrefab, gameObject.transform.position, Quaternion.identity);
            }
            Destroy(other.gameObject);
            float restartDelay = 1f;
            Invoke("transtionor", restartDelay);
        }
    }

    private void transtionor()
    {   
        transition.SetTrigger("start");
        float restartDelay = 0.6f;
        Invoke("LoadNextScene", restartDelay);
    }

    // Function to load the next scene
    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if there is a next scene in the build order
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // You've reached the end of the scenes
            Debug.Log("You've completed all scenes!");
        }
    }
}
