using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D playerRigidBody;
    public float speed;
    private float horizontal;

    private bool facingRight;

    private bool jump = false;
    public float jumpForce;
    public Transform groundCheck;
    public bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
  
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGround)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        MovePlayer(horizontal);

        if (jump && isGround)
        {
            JumpPlayer();
        }

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
            if(collision.gameObject.tag == "Chao")
        {
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }

    void MovePlayer(float move){
        playerRigidBody.velocity = new Vector2(move * speed, playerRigidBody.velocity.y);

        if(move > 0 && facingRight || move < 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x , transform.localScale.y, transform.localScale.z);
    }

    void JumpPlayer()
    {
        if (isGround)
        {
            playerRigidBody.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }
}
