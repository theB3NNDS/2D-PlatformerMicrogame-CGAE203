using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private float dirX = 0f;
    private bool facingRight = true;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling };

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();

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

        anim.SetInteger("state", (int)state);

    }

    private void Flip()
	    {
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
	    }

}
