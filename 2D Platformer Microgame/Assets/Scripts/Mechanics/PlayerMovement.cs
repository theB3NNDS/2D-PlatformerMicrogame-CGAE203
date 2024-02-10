using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private float dirX = 0f;
    private bool facingRight = true;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState { idle, running, jumping, falling, double_jump };
    private bool doubleJump = true;
    private int jumpcounter = 0;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        MovementState movement;

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpcounter++;
        }
        else if (Input.GetButtonDown("Jump") && !IsGrounded() && doubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * 0.8f);
            doubleJump = false;
            jumpcounter++;
        }

        UpdateAnimationState();
        
        if (IsGrounded())
            doubleJump = true;
            jumpcounter = 0;

        //player flipping
        if (dirX > 0.01f && !facingRight)
            Flip();        
        else if (dirX < -0.01f && facingRight)
            Flip();
        
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (Input.GetButton("Horizontal"))
            state = MovementState.running;
        else 
            state = MovementState.idle;

        if (rb.velocity.y > 0.1f)
            state = MovementState.jumping;
        else if (rb.velocity.y < -0.1f)
            state = MovementState.falling;

        if (jumpcounter == 1)
            state = MovementState.double_jump;
        
        anim.SetInteger("state", (int)state);

    }

    private void Flip()
	    {
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
	    }
    
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    

}