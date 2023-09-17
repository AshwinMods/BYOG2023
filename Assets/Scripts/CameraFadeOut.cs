using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFadeOut : MonoBehaviour
{
     public float fadeDuration = 2.0f;
    public bool destroyAfterFade = true;

    private Image uiImage;

    private void Start()
    {
        uiImage = GetComponent<Image>();

        // Start the fade out process
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color startColor = uiImage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < fadeDuration)
        {
            uiImage.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final color is the target color
        uiImage.color = targetColor;

        // Destroy the GameObject after fading if specified
        if (destroyAfterFade)
        {
            Destroy(gameObject);
        }
    }
}
