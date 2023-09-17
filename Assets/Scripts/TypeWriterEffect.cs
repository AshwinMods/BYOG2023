using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
        public TMP_Text textComponent;
    [TextArea] public string[] textLines;
    public float typingSpeed = 50f;
    public float lineDelay = 1f;
    public AudioClip typewriterSound;
    private AudioSource audioSource;

    private bool isTyping = false;
    private int currentLineIndex = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartTypewriterWithDelay());
    }

    IEnumerator StartTypewriterWithDelay()
    {
        yield return new WaitForSeconds(2f); // Add a delay of 2 seconds before starting.
        StartCoroutine(TypeText());
    }

    private void Update()
    {
        // Skip typing animation when the player clicks.
        if (Input.GetMouseButtonDown(0) && isTyping)
        {
            StopAllCoroutines();
            textComponent.text = textLines[currentLineIndex];
            isTyping = false;
            audioSource.Stop(); // Stop the typewriter sound when skipping.
        }
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        string currentLine = textLines[currentLineIndex];
        int charIndex = 0;

        while (charIndex < currentLine.Length)
        {
            if (currentLine[charIndex] == '<')
            {
                // Parse the rich text tag.
                int endIndex = currentLine.IndexOf('>', charIndex);
                if (endIndex != -1)
                {
                    textComponent.text += currentLine.Substring(charIndex, endIndex - charIndex + 1);
                    charIndex = endIndex + 1;
                }
            }
            else
            {
                textComponent.text += currentLine[charIndex];
                audioSource.PlayOneShot(typewriterSound); // Play typewriter sound for each character.
                charIndex++;
            }

            yield return new WaitForSeconds(1f / typingSpeed);
        }

        isTyping = false;
        yield return new WaitForSeconds(lineDelay);

        currentLineIndex++;

        if (currentLineIndex < textLines.Length)
        {
            textComponent.text = "";
            StartCoroutine(TypeText());
        }
    }
}
