using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public int jumpCount;
    private int jumpsLeft;

    public float speed;
    public float jumpForce;
    public float checkRadius;

    private float moveInput;
    private float fallCheck;

    public float jumpTime;
    private float jumpTimeCounter;

    public bool moveStop;
    public bool isGrounded;
    public bool isJumping;
    public bool isFalling;

    public Animator animator;
    public SpriteRenderer renderer;

    public Transform feetPos;
    public LayerMask whatIsGround;

    //Fall Check Variables
    Vector3 currentPos;
    Vector3 newPos;

    private Rigidbody2D rigidbody;


    private void Start()
    {
        jumpsLeft = jumpCount;
        jumpTimeCounter = jumpTime;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        animator.SetBool("IsGrounded", isGrounded);

        if (!moveStop)
        {
            if (isJumping) animator.SetTrigger("Jump");

            isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

            if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;

                rigidbody.velocity = Vector2.up * jumpForce;

                jumpsLeft--;
            }
            else if (jumpsLeft > 0 && Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;

                rigidbody.velocity = Vector2.up * jumpForce;

                jumpsLeft--;
            }

            if (isGrounded) jumpsLeft = jumpCount;

            if (Input.GetKey(KeyCode.Space) && isJumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    rigidbody.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else isJumping = false;
            }
            else if (Input.GetKeyUp(KeyCode.Space)) isJumping = false;

            bool falls = IsFalling();

            if (falls) animator.SetTrigger("Fall");
        }
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");

        if(moveInput != 0 && !moveStop)
            {
            animator.SetBool("IsRunning", true);
        }
            else
        {
            animator.SetBool("IsRunning", false);
        }

        if (!moveStop)
        {
            if (moveInput < 0) renderer.flipX = true;
            else if (moveInput > 0) renderer.flipX = false;

            rigidbody.velocity = new Vector2(moveInput * speed, rigidbody.velocity.y);
        }
        else rigidbody.velocity = Vector2.zero;
    }

    bool IsFalling()
    {
        newPos = transform.position;

        if (currentPos.y > newPos.y) isFalling = true;
        else isFalling = false;

        currentPos = transform.position;

        return isFalling;
    }
}
