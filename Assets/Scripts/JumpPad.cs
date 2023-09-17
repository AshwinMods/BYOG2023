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
			collision.attachedRigidbody.velocity = (transform.up * JumpForce);

		if (bounce != null)
            {
                bounce.Play();
            }
	}
}
