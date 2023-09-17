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
		Rigidbody2D otherRigidbody = collision.attachedRigidbody;

    // Check if the collision has an attached rigidbody and it's not the same as the script's GameObject's rigidbody
    if (otherRigidbody != null && otherRigidbody != GetComponent<Rigidbody2D>())
    {
			collision.attachedRigidbody.velocity = (transform.up * JumpForce);

		if (bounce != null)
            {
                bounce.Play();
            }
	}
}
}
