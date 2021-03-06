﻿using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

public class IAEnemie : MonoBehaviour
{

    public GameObject player;
    public GameObject enemieObject;
    public Transform spotPosition;
    public Animator enemieAnimator;
    public bool attackingPlayer;
    public AudioSource fxGame;
    public AudioClip fxAttacking;


    // Start is called before the first frame update
    void Start() 
    { 
        enemieAnimator = GetComponent<Animator>();
        attackingPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Attack();
            }
        }*/

    private void OnCollisionStay2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "Player")
        {
            if(attackingPlayer == false)
            {
                StartCoroutine("AttackCorroutine");
            }    
        }
    }

 
    void Attack()
    {
        enemieAnimator.SetBool("Walking", false);
        enemieAnimator.SetBool("Attacking", true);
        attackingPlayer = true;
        
    }

    void StopAttack()
    {
        enemieAnimator.SetBool("Walking", true);
        enemieAnimator.SetBool("Attacking", false);
        attackingPlayer = false;
    }


    IEnumerator AttackCorroutine()
    {      
        Attack();
        fxGame.PlayOneShot(fxAttacking);
        yield return new WaitForSeconds(1f);
        StopAttack();
    }
}