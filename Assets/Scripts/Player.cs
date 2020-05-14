using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    public float speed;
    private float horizontal;

    private bool facingRight;

    private bool jump = false;
    public float jumpForce;
    public bool isGround;
    private bool maxJump = false;
    private Collision2D playerCollision;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
       
  
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            JumpPlayer();
        }

        Deslizar(playerCollision);
    }

    void FixedUpdate()
    {
      
        if (horizontal != 0) {
            MovePlayer(horizontal);
        }
        else
        {
            playerAnimator.SetBool("Running", false);
        }
    }



    void MovePlayer(float move){
        playerRigidBody.velocity = new Vector2(move * speed, playerRigidBody.velocity.y);

        if(move > 0 && facingRight || move < 0 && !facingRight)
        {
            Flip();
        }
        playerAnimator.SetBool("Running", true);

    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x , transform.localScale.y, transform.localScale.z);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerCollision = collision;
        if (collision.gameObject.tag == "Chao")
        {
            isGround = true;
            maxJump = false;
            playerAnimator.SetBool("Jump", false);
            playerAnimator.SetBool("IsGrounded", true);
        }
       
    }

    void JumpPlayer()
    {

        jump = true;
        if (!maxJump)
        {
            playerRigidBody.AddForce(new Vector2(0f, jumpForce));
            playerAnimator.SetBool("Jump", true);
            playerAnimator.SetBool("IsGrounded", false);
            maxJump = true;
        }  
    }

    void Deslizar(Collision2D collision)
    {
        if (collision.gameObject.name == "Rampa" && facingRight == false)
        {
            speed = 10;
            playerAnimator.SetBool("Sliding", true);
            playerAnimator.SetBool("Running", false);
        }
        if(facingRight == true)
        {
            speed = 5;
            playerAnimator.SetBool("Sliding", false);
            playerAnimator.SetBool("Running", true);
        }
        if(horizontal == 0)
        {
            playerAnimator.SetBool("Sliding", false);
            playerAnimator.SetBool("Running", false);
        }
    }

}
