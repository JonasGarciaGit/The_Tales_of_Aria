using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAMinotaur : MonoBehaviour
{

    bool playerEnterZone;
    public GameObject player;
    public GameObject enemieObject;
    Transform enemie;
    public float speed;
    private SpriteRenderer enemieSprite;
    public Transform spotPosition;
    bool stalkingPlayer;
    bool isRight;
    private Animator enemieAnimator;

    // Start is called before the first frame update
    void Start()
    {
        stalkingPlayer = false;
        enemieSprite = GetComponent<SpriteRenderer>();
        enemie = GetComponent<Transform>();
        enemieAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isRight = enemieObject.GetComponent<Enemie>().isRight;
        playerEnterZone = player.GetComponent<Player>().enemieSpotCollide;
        if (enemie != null)
        {      
                SeguirPlayer();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Attack();
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StopAttack();
        }
    }

    void SeguirPlayer()
    {
        if (playerEnterZone == true)
        {
            enemie.position = Vector3.MoveTowards(enemie.position,new Vector3(player.transform.position.x , enemie.position.y , enemie.position.z)
                , speed * Time.deltaTime);

            if (player.transform.position.x < enemie.position.x && isRight == true)
            {
                enemieObject.GetComponent<Enemie>().Flip();
            }

            if (player.transform.position.x > enemie.position.x && isRight == false)
            {
                enemieObject.GetComponent<Enemie>().Flip();
            }

            stalkingPlayer = true;
        }
        if(playerEnterZone == false && stalkingPlayer == true)
        {
            stalkingPlayer = false;
            enemieObject.GetComponent<Enemie>().Movimentação();
        }


    }

    void Attack()
    {
        enemieAnimator.SetBool("Walking", false);
        enemieAnimator.SetBool("Attacking", true);
        enemie.position = Vector3.MoveTowards(enemie.position, enemie.position, 0);
    }

    void StopAttack()
    {
        enemieAnimator.SetBool("Walking", true);
        enemieAnimator.SetBool("Attacking", false);
    }

}
