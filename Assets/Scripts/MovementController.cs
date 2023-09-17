using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private float horizontal;
	public Animator animator;
    private bool isFacingRight = true;
	 public SpriteRenderer spriteRenderer;

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

	public bool hasJumped = false;

	public bool canAnimate = false;
	private void Awake()
	{
        m_CameraTrans = Camera.main.transform;
	}

	private void Start()
    {
        // Get the SpriteRenderer component attached to the player GameObject
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }
	void Update()
    {
		if (hasJumped && IsGrounded() && rb.velocity.y<=0)
		{
			hasJumped = false;
			if(canAnimate)
			animator.SetBool("IsJumping",false);
		}

		if (CanJump) Jump();
        if(CanFlip) Flip();
        if(CanTilt) Tilt();

		if(canAnimate)
		animator.SetFloat("Speed",Mathf.Abs(horizontal));
				
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
		rb.velocity = new Vector2(horizontal * speed * Time.deltaTime, rb.velocity.y);
	}
	private void Jump()
	{
		
		if (Input.GetButtonDown("Jump") && IsGrounded())
		{
			hasJumped = true;
			rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
			if(canAnimate)
			animator.SetBool("IsJumping",true);
		}

		if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
			//animator.SetBool("IsJumping",true);
		}
	}


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
		
    }
    private void Flip()
    {
		transform.up = m_CameraTrans.up;

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
			


            isFacingRight = !isFacingRight;
			// Flip the player's sprite horizontally
        	spriteRenderer.flipX = !spriteRenderer.flipX;
           // Vector3 localScale = transform.localScale;
            //localScale.x *= -1f;
            //transform.localScale = localScale;
        }
    }
}
