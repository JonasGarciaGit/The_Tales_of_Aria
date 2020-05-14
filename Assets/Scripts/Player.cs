using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    public float speed;
    private float horizontal;
    public Image life;
    public float ActualLife;
    public float MaxLife = 182.0f;
    private float deadlyDamage = 0.0f;
    private bool facingRight;
    private Transform myTransform;
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
        myTransform = GetComponent<Transform>();
        ActualLife = MaxLife;

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            JumpPlayer();
        }
        if (ActualLife > 0 && ActualLife <= MaxLife)
        {
            life.rectTransform.sizeDelta = new Vector2(ActualLife / MaxLife * 182, 11.43732f);
        }

        Deslizar(playerCollision);
        Curar();
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
        if (collision.gameObject.tag == "Chao" || collision.gameObject.tag == "plataformaMovel") 
        {
            isGround = true;
            maxJump = false;
            playerAnimator.SetBool("Jump", false);
            playerAnimator.SetBool("IsGrounded", true);
        }
        if (collision.transform.tag == "plataformaMovel")
        {
            myTransform.parent = collision.transform;
        }
        if (collision.gameObject.tag == "arma")
        {
            ActualLife = ActualLife - 20;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "plataformaMovel")
        {
            myTransform.parent = null;
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

    void Curar()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActualLife = ActualLife + 20;
        }
    }

}
