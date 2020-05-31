using JetBrains.Annotations;
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




    //variaveis necessárias para o controle do attack
    float tempEnemieSpeed;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Attack();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopAttack();
        }
    }

    void Attack()
    {
        enemieAnimator.SetBool("Walking", false);
        enemieAnimator.SetBool("Attacking", true);
        attackingPlayer = true;

        //Speed no script Enemie
        tempEnemieSpeed = enemieObject.GetComponent<EnemieMoviment>().speed;
        enemieObject.GetComponent<EnemieMoviment>().speed = 0;

    }

    void StopAttack()
    {
        enemieAnimator.SetBool("Walking", true);
        enemieAnimator.SetBool("Attacking", false);
        attackingPlayer = false;

        //Manipulando velocidade do script inimigo
        enemieObject.GetComponent<EnemieMoviment>().speed = tempEnemieSpeed;

    }

}