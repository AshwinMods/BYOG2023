using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class JumpPad : MonoBehaviour
{
	public AudioSource bounce;
	public float JumpForce = 5;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.attachedRigidbody != null)
			collision.attachedRigidbody.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);

		if (bounce != null)
            {
                bounce.Play();
            }
	}
}
