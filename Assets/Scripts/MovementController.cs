using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private float horizontal;
    private bool isFacingRight = true;

	public bool CanFlip = false;
	[Space]
	public bool CanMove = false;
	public bool CanMoveLeft = true;
	public bool CanMoveRight = true;
	public float speed = 8f;
	[Space]
	public bool CanJump = false;
	public float jumpingPower = 16f;
	[Space]
	public bool CanTilt = false;
	public float TiltSpeed = 16f;
	[Space]
	public bool UseExternalForce = false;
	public float externalForceMul = 4f;

	[SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    Transform m_CameraTrans;
	Vector2 m_ExternalForce = Vector2.zero;
	private void Awake()
	{
        m_CameraTrans = Camera.main.transform;
	}
	void Update()
    {
		if (CanJump) Jump();
        if(CanFlip) Flip();
        if(CanTilt) Tilt();
	}

	private void FixedUpdate()
	{
        if (UseExternalForce) ExternalForce();
        if (CanMove) Move();
	}

	private void ExternalForce()
	{
		m_ExternalForce = -m_CameraTrans.up;
		rb.AddForce(m_ExternalForce * externalForceMul);
	}
	private void Tilt()
	{
		rb.rotation += Input.GetAxisRaw("Horizontal") * TiltSpeed;
	}
	private void Move()
	{
		horizontal = Input.GetAxisRaw("Horizontal");
		if (!CanMoveLeft) horizontal = Mathf.Max(0, horizontal);
		if (!CanMoveRight) horizontal = Mathf.Min(0, horizontal);
		rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
	}
	private void Jump()
	{
		if (Input.GetButtonDown("Jump") && IsGrounded())
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
		}

		if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
		}
	}
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
