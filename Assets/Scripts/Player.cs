﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Rigidbody2D playerRigidBody;
    public Animator playerAnimator;
    public float speed = 2;
    public float horizontal;
    public Image life;
    public Image mana;
    public float ActualMana;
    public float MaxMana = 182.0f;
    public float ActualLife;
    public float MaxLife = 182.0f;
    private bool damage = true;
    private float deadlyDamage = 0.0f;
    public Image Exp;
    public float ActualExp;
    public float MaxExp = 183.0f;
    public Text Nivel;
    public Text AppleAmount;
    public bool facingRight;
    public Transform myTransform;
    private bool jump = false;
    public float jumpForce;
    public bool isGround;
    private bool maxJump = false;
    private Collision2D playerCollision;
    private bool IsRunning;
    private SpriteRenderer PlayerSpriteRender;
    public GameObject Armas;
    public GameObject Enemie;
    public float WeaponDamage;
    private string enemieName;
    public bool enemieSpotCollide;
    public Inventory inventory;
    public UI_Inventory uiInventory;
    public static Player Instance { get; private set; }
    public GameObject InventoryCanvas;
    public bool activeInventory;
    public int fireBoolCooldown;
    public string enemieSpotName;
	public GameObject windCutPrefabDir;
    public GameObject windCutPrefabEsq;
    public GameObject effectLevelUp;
    public AudioSource fxGame;
    public AudioClip windCutAudio;
    public GameObject menuInGame;
    public bool activeMenuInGame;
    public GameObject HealingEffect;
    public GameObject ManaEffect;
    public bool canUseMagic;
    public Image fireMagicImage;
    public Image windMagicImage;
    public bool firstTimeAweaking;


    private void Awake()
    {
        Instance = this;
        inventory = new Inventory(UseItem);
        uiInventory.SetInventory(inventory);
        uiInventory.SetPlayer(this);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        PlayerSpriteRender = GetComponent<SpriteRenderer>();
        ActualLife = MaxLife;
        ActualMana = MaxMana;
        activeInventory = false;
        activeMenuInGame = false;
        InventoryCanvas.SetActive(false);
        menuInGame.SetActive(false);
        fireBoolCooldown = 0;
        canUseMagic = false;

        
       
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                if(ActualLife < MaxLife)
                {
                    inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                    HealthWithPotion();
                }
                break;
            case Item.ItemType.ManaPotion:
                if(ActualMana < MaxMana)
                {
                    inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                    RecoveryMP();
                }
                break;
            case Item.ItemType.Apple:
                if(ActualLife < MaxLife)
                {
                    HealthWithApple();
                    inventory.RemoveItem(new Item { itemType = Item.ItemType.Apple, amount = 1 });
                }
                break;
        }
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
        if (ActualMana > 0 && ActualMana <= MaxMana)
        {
            mana.rectTransform.sizeDelta = new Vector2(ActualMana / MaxMana * 182, 11.43732f);
        }
        if (Input.GetKeyDown(KeyCode.R) && canUseMagic == true)
        {
            fxGame.PlayOneShot(windCutAudio);
            StartCoroutine("playerSlashing", true);
        }
        if (firstTimeAweaking == true)
        {
            StartCoroutine("executeAwakingAnim");
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
        if (Input.GetKeyDown(KeyCode.F) && playerAnimator.GetBool("IsGrounded") == true && fireBoolCooldown == 0 && canUseMagic == true)
        {
            try
            {
                this.gameObject.GetComponent<FireBallMagic>().StartCoroutine("SpellMagic", true);
                ActualMana = this.gameObject.GetComponent<FireBallMagic>().ActualMana;
                StartCoroutine("ConjuringFireballAnimationControl", true);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        if (ActualLife <= 0)
        {
            Invoke("RecarregarJogo", 4f);
            gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (activeInventory == false)
            {
                InventoryCanvas.SetActive(true);
                activeInventory = true;
            }
            else
            {
                InventoryCanvas.SetActive(false);
                activeInventory = false;
            }


        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(activeMenuInGame == false)
            {
                menuInGame.SetActive(true);
                activeMenuInGame = true;
            }
            else{
                menuInGame.SetActive(false);
                activeMenuInGame = false;
            }
        }


        try
        {
            fireMagicImage = GameObject.Find("MagiaFogo").GetComponent<Image>();
            windMagicImage = GameObject.Find("MagiaVento").GetComponent<Image>();

            if (canUseMagic == false)
            {
                fireMagicImage.color = new Color32(255, 255, 255, 60);     
                windMagicImage.color = new Color32(0, 255, 0, 60);
            }
            else
            {
                fireMagicImage.color = new Color32(255, 255, 255, 255);
                windMagicImage.color = new Color32(0, 255, 0, 255);
            }

        }
        catch(Exception e)
        {
            
        }

        inputShiftCorrer();
        levelUP();
        Deslizar(playerCollision);
    }

    void FixedUpdate()
    {
        weaponDamage();

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
        if (firstTimeAweaking == false)
        {
            playerRigidBody.velocity = new Vector2(move * speed, playerRigidBody.velocity.y);

            if (move > 0 && facingRight || move < 0 && !facingRight)
            {
                Flip();
            }
            playerAnimator.SetBool("Walking", true);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
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
            if(collision.gameObject.tag == "Anel")
            {
                canUseMagic = true;
            }


            if (collision.gameObject.tag == "Enemie")
            {
                if (Enemie != null)
                {
                    if (collision.gameObject.name == Enemie.name)
                    {
                        Enemie = Enemie;
                    }
                    else
                    {
                        Enemie = collision.gameObject;
                    }
                }
                else
                {
                    Enemie = collision.gameObject;
                }
            }
        }
        catch (Exception e)
        {

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "OutOfZone" && enemieSpotCollide == true)
        {
            enemieSpotCollide = false;
            Enemie = null;
        }
        if (collision.transform.tag == "EnemieSpot" && enemieSpotCollide == false)
        {
            enemieSpotCollide = true;
            enemieSpotName = collision.gameObject.name;
        }

        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            //Tocando o item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
        if(collision.gameObject.tag == "Coin")
        {
            inventory.AddItem(new Item {itemType = Item.ItemType.Coin, amount = collision.gameObject.GetComponent<AmountCoinsDrop>().amountCoins});
            Destroy(collision.gameObject);
        }
        if(canUseMagic == true && collision.gameObject.tag == "NpcVelho")
        {
            Destroy(collision.gameObject.GetComponent<Conversa>());
        }
		
		if (collision.gameObject.tag == "BossFireBallCircle")
        {
            ActualLife = ActualLife - 30;
            StartCoroutine("Invencible");
        }
        if (collision.gameObject.tag == "BossFireBall")
        {
            ActualLife = ActualLife - 10;
            StartCoroutine("Invencible");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemie" && damage == true && Enemie.GetComponent<IAEnemie>().enemieAnimator.GetBool("Attacking") == true)
        {
            ActualLife = ActualLife - 20;
            StartCoroutine("Invencible");
        }
    }

    void JumpPlayer()
    {
        InventoryCanvas.SetActive(false);
        activeInventory = false;
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

    void HealthWithApple()
    {
        if(ActualLife + 20 >= MaxLife)
        {
            ActualLife = MaxLife;
        }
        else
        {
            ActualLife = ActualLife + 20;
        }
        GameObject healingEffect = Instantiate(HealingEffect,myTransform.position,Quaternion.identity);
        healingEffect.transform.parent = myTransform;
        Destroy(healingEffect,3.0f);
    }

    void HealthWithPotion()
    {
        if (ActualLife + 50 >= MaxLife)
        {
            ActualLife = MaxLife;
        }
        else
        {
            ActualLife = ActualLife + 50;
        }
        GameObject healingEffect = Instantiate(HealingEffect, myTransform.position, Quaternion.identity);
        healingEffect.transform.parent = myTransform;
        Destroy(healingEffect, 3.0f);
    }

    void RecoveryMP()
    {
        if(ActualMana + 50 >= MaxMana)
        {
            ActualMana = MaxMana;
        }
        else
        {
            ActualMana = ActualMana + 50;
        }
        GameObject manaEffect = Instantiate(ManaEffect, myTransform.position, Quaternion.identity);
        manaEffect.transform.parent = myTransform;
        Destroy(manaEffect, 3.0f);
    }

    void inputShiftCorrer()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsRunning = true;
            Correr(IsRunning);
            speed = 5;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || horizontal == 0f)
        {
            IsRunning = false;
            Correr(IsRunning);
            speed = 2;
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


    void levelUP()
    {
        if (ActualExp <= MaxExp)
        {
            Exp.rectTransform.sizeDelta = new Vector2(ActualExp / MaxExp * 208f, 9.60f);
        }
        else { 
        if (ActualExp <= 0)
          {
                ActualExp = 0;
          }
        if (ActualExp >= MaxExp)
            {
                ActualExp = ActualExp - MaxExp;
                Exp.rectTransform.sizeDelta = new Vector2(ActualExp / MaxExp * 208f, 9.60f);
            }
            GameObject effect = Instantiate(effectLevelUp,new Vector3(myTransform.position.x,myTransform.position.y -0.5f,myTransform.position.z),Quaternion.identity);
            effect.transform.Rotate(new Vector3(-90, 0, 0));
            effect.transform.parent = myTransform;
            int Levelup = int.Parse(Nivel.text) + 1;
            Nivel.text = Levelup.ToString();
            ActualLife = MaxLife;
            ActualMana = MaxMana;
            MaxExp = MaxExp * 1.2f;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
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

    public float weaponDamage()
    {
        try
        {
            WeaponDamage = Armas.GetComponent<Armas>().weaponDamage;
        }
        catch (Exception e)
        {

        }        return WeaponDamage;
    }

    public void openInventory()
    {
        if (activeInventory == false)
        {
            InventoryCanvas.SetActive(true);
            activeInventory = true;
        }
        else
        {
            InventoryCanvas.SetActive(false);
            activeInventory = false;
        }
    }

    IEnumerator ConjuringFireballAnimationControl()
    {
        playerAnimator.SetBool("Conjuring", true);
        fireBoolCooldown = 1;
        float tempSpeed = speed;
        speed = 0;

        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetBool("Conjuring", false);
        fireBoolCooldown = 0;

        if (playerAnimator.GetBool("Walking") == true)
        {
            speed = 2;
        }
        if (playerAnimator.GetBool("Running") == true)
        {
            speed = 5;
        }

    }

 IEnumerator playerSlashing()
    {
        playerAnimator.SetBool("Attacking", true);
        GameObject tempWindCut = null;

        if (facingRight == false)
        {
          tempWindCut = Instantiate(windCutPrefabDir, new Vector3(myTransform.position.x + 1f, myTransform.position.y + 0.3f, myTransform.position.z), myTransform.localRotation);
          
        }
        else if (facingRight == true)
        {
            tempWindCut = Instantiate(windCutPrefabEsq, new Vector3(myTransform.position.x - 1f, myTransform.position.y + 0.3f, myTransform.position.z), myTransform.localRotation);
        }

        yield return new WaitForSeconds(0.2f); 
        playerAnimator.SetBool("Attacking", false);
        Destroy(tempWindCut);

    }

    IEnumerator executeAwakingAnim()
    {
        if(firstTimeAweaking == true)
        {
            playerAnimator.SetBool("isAwake", true);
        }
        else
        {
            playerAnimator.SetBool("isAwake", false);
        }
        
        yield return new WaitForSeconds(1.3f);
        playerAnimator.SetBool("isAwake", false);
        firstTimeAweaking = false;
    }


}
