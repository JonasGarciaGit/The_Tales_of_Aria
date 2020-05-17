using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    public float speed = 2;
    private float horizontal;
    public Image life;
    public float ActualLife;
    public float MaxLife = 182.0f;
    private bool damage = true;
    private float deadlyDamage = 0.0f;
    public Image Exp;
    public float ActualExp;
    public float MaxExp = 183.0f;
    public Text Nivel;
    public Text AppleAmount;
    private bool facingRight;
    private Transform myTransform;
    private bool jump = false;
    public float jumpForce;
    public bool isGround;
    private bool maxJump = false;
    private Collision2D playerCollision;
    private bool IsRunning;
    private SpriteRenderer PlayerSpriteRender;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        PlayerSpriteRender = GetComponent<SpriteRenderer>();
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsRunning = true;
            Correr(IsRunning);
            speed = 5;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsRunning = false;
            Correr(IsRunning);
            speed = 2;
        }

        Experience();
        Deslizar(playerCollision);
        Curar();
    }

    void FixedUpdate()
    {

        if (horizontal != 0)
        {
            MovePlayer(horizontal);
        }
        else
        {
            playerAnimator.SetBool("Walking", false);
        }
    }



    void MovePlayer(float move)
    {
        playerRigidBody.velocity = new Vector2(move * speed, playerRigidBody.velocity.y);

        if (move > 0 && facingRight || move < 0 && !facingRight)
        {
            Flip();
        }
        playerAnimator.SetBool("Walking", true);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
        if (collision.gameObject.tag == "arma" && damage == true)
        {
            ActualLife = ActualLife - 20;
            StartCoroutine("Invencible");
        }
        if (ActualLife <= 0)
        {
            Invoke("RecarregarJogo", 4f);
            gameObject.SetActive(false);
        }
    }

    void RecarregarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        try
        {
            if (collision.gameObject.name == "Rampa" && facingRight == false)
            {
                speed = 10;
                playerAnimator.SetBool("Sliding", true);
                playerAnimator.SetBool("Walking", false);
                playerAnimator.SetBool("Running", false);
            }
            if (collision.gameObject.name == "Rampa" && facingRight == true)
            {
                playerAnimator.SetBool("Sliding", false);
                playerAnimator.SetBool("Walking", true);

                if (playerAnimator.GetBool("Walking") == true)
                {
                    speed = 2;
                }
                if (playerAnimator.GetBool("Running") == true)
                {
                    speed = 5;
                }

            }
            if (horizontal == 0)
            {
                playerAnimator.SetBool("Sliding", false);
                playerAnimator.SetBool("Walking", false);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    void Curar()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && int.Parse(AppleAmount.text) > 0)
        {
            ActualLife = ActualLife + 20;
            AppleAmount.text = (int.Parse(AppleAmount.text) - 1).ToString();
        }
    }

    void Correr(bool isRunning)
    {
        if (isRunning)
        {
            playerAnimator.SetBool("Running", true);
            playerAnimator.SetBool("Walking", false);
        }
        if (!isRunning)
        {
            playerAnimator.SetBool("Running", false);
            playerAnimator.SetBool("Walking", true);
        }
    }


    void Experience()
    {
        if (ActualExp <= MaxExp)
        {
            Exp.rectTransform.sizeDelta = new Vector2(ActualExp / MaxExp * 183, 9.60f);
        }
        else
        {
            ActualExp = 0;
            int Levelup = int.Parse(Nivel.text) + 1;
            Nivel.text = Levelup.ToString();
            MaxExp = MaxExp * 1.8f;
        }
    }

    //Criando uma corotina para deixar o personagem invencivel após sofrer dano.
    IEnumerator Invencible()
    {
        for (float i = 0; i < 1; i += 0.1f)
        {
            damage = false;
            PlayerSpriteRender.enabled = false;
            yield return new WaitForSeconds(0.1f);
            PlayerSpriteRender.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        damage = true;
    }

}
