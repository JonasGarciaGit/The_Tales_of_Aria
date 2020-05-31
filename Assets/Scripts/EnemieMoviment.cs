using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieMoviment : MonoBehaviour
{

    public Transform enemie;
    public SpriteRenderer enemieSprite;
    public Transform[] position;
    public float speed;
    public bool isRight;
    public GameObject player;
    private bool playerIsRight;
    private int idTarget; // alvo


    // Variaveis da função SeguirPlayer()
    private bool playerEnterZone;
    public string enemieSpotName;
    private string playerColliderSpotName;
    public GameObject enemieObject;

    // Start is called before the first frame update
    void Start()
    {
        enemieSprite = enemie.gameObject.GetComponent<SpriteRenderer>();
        enemie.position = position[0].position;
        idTarget = 1;

    }

    // Update is called once per frame
    void Update()
    {
        //Variaveis
        playerEnterZone = player.GetComponent<Player>().enemieSpotCollide;
        playerColliderSpotName = player.GetComponent<Player>().enemieSpotName;
       // playerIsRight = player.GetComponent<Player>().facingRight;

        // Funções

        if (enemie != null && playerEnterZone == false)
        {
            Movimentação();
        }

        if (enemie != null && playerEnterZone == true)
        {
            SeguirPlayer();
        }
    }

    public void Flip()
    {
        isRight = !isRight;
        enemieSprite.flipX = !enemieSprite.flipX;
    }

    public void Movimentação()
    {
        if (enemie != null)
        {
       
            enemie.position = Vector3.MoveTowards(enemie.position, position[idTarget].position, speed * Time.deltaTime);

            if (enemie.position == position[idTarget].position)
            {
                idTarget += 1; // 1
                if (idTarget == position.Length)
                {
                    idTarget = 0;
                }
            }

            if (position[idTarget].position.x < enemie.position.x && isRight == true)
            {
                Flip();
            }
            else if (position[idTarget].position.x > enemie.position.x && isRight == false)
            {
                Flip();
            }

        }
    }



    void SeguirPlayer()
    {
        if (playerEnterZone == true && enemieSpotName == playerColliderSpotName)
        {
            enemie.position = Vector3.MoveTowards(enemie.position, new Vector3(player.transform.position.x, enemie.position.y, enemie.position.z), speed * Time.deltaTime);

            if (player.transform.position.x < enemie.position.x && isRight == true)
            {
                Flip();
            }
            else if (player.transform.position.x > enemie.position.x && isRight == false)
            {
                Flip();  
            }
        }
    }

}