using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    float rotateSpeed = 40;
    public Transform fireCircle;
    public GameObject fireCircleObj;
    public Transform[] movementPoints;
    public Transform[] fireballs;
    private float fireBallSpeed;
    public GameObject player;
    public int idTarget;
    public Transform enemie;
    float speed = 1;
    public GameObject fireballPrefab;
    public GameObject explosionPrefab;
    private bool timeAttack;
    float x;
    float y;
    private bool bossStandby;
    private int standByControll;
    public AudioSource fxGame;
    private bool cinematic;
    public GameObject bossObjForCustcene;
    private bool canMove;
    private bool taunting;
    int timeExecutedCinematic;
    public GameObject canvas;
    public GameObject BarraCinematic1;
    public GameObject BarraCinematic2;
    public GameObject powerUp1;
    public GameObject powerUp2;

    //FIREBALLS
    GameObject tempFireballs1;
    GameObject tempFireballs2;
    GameObject tempFireballs3;

    GameObject explosion1;
    GameObject explosion2;
    GameObject explosion3;

    public AudioClip explosionAudio;
    public AudioClip fireBallAudio;

    private void Start()
    {
        timeAttack = true;
        bossStandby = false;
        canMove = false;
        timeAttack = false;
        taunting = true;
        timeExecutedCinematic = 0;
        BarraCinematic1.SetActive(false);
        BarraCinematic2.SetActive(false);
        GameObject.Find("PowerUp1").SetActive(false);
        GameObject.Find("PowerUp2").SetActive(false);

        bossObjForCustcene.transform.position = new Vector3(19,11,-0.450f);
    }

    // Update is called once per frame
    void Update()
    {
        cinematic = GameObject.Find("Cinematic").GetComponent<CinematicBool>().cinematic;
        float bossLife = GetComponent<EnemieDeath>().enemieLife;

        fireCircle.transform.Rotate(0, 0, Time.deltaTime * rotateSpeed);
        if (!bossStandby)
        {
            if (timeAttack)
            {

                var instantiatePoint = Random.Range(0, 8);
                var instantiatePoint1 = Random.Range(0, 8);
                var instantiatePoint2 = Random.Range(0, 8);

                tempFireballs1 = Instantiate(fireballPrefab, fireballs[instantiatePoint].position, fireballs[instantiatePoint].localRotation);
                tempFireballs2 = Instantiate(fireballPrefab, fireballs[instantiatePoint1].position, fireballs[instantiatePoint1].localRotation);
                tempFireballs3 = Instantiate(fireballPrefab, fireballs[instantiatePoint2].position, fireballs[instantiatePoint2].localRotation);
                
                fxGame.PlayOneShot(fireBallAudio,1);
              
                x = player.transform.position.x;
                y = player.transform.position.y;
            }


            Attack(x -5, y);
            Movimentação();
            Cinematic();

        }

        if(bossLife <= 0)
        {
            Destroy(tempFireballs1);
            Destroy(tempFireballs2);
            Destroy(tempFireballs3);
            Destroy(explosion1);
            Destroy(explosion2);
            Destroy(explosion3);
        }

    }

    void Movimentação()
    {
        if(canMove == true)
        {
            enemie.position = Vector3.MoveTowards(enemie.position, movementPoints[idTarget].position, speed * Time.deltaTime);

            if (enemie.position == movementPoints[idTarget].position)
            {
                idTarget += 1; // 1
                if (idTarget == movementPoints.Length)
                {
                    idTarget = 0;
                }
                standByControll++;
            }
            if (standByControll == 5)
            {
                StartCoroutine("StopAttack");
                standByControll = 0;
            }
        }
    }


    void Attack(float x, float y)
    {
        timeAttack = false;

        if (tempFireballs1 != null &&
            tempFireballs2 != null &&
            tempFireballs3 != null)
        {
            if (x <= 10)
            {
                fireBallSpeed = 10;
            }
            else
            {
                fireBallSpeed = 5;
            }

            GameObject.Find("Boss").GetComponent<Animator>().SetBool("Attacking", true);
            tempFireballs1.transform.position = Vector3.MoveTowards(tempFireballs1.transform.position, new Vector2(x, y), fireBallSpeed * Time.deltaTime);
            tempFireballs2.transform.position = Vector3.MoveTowards(tempFireballs2.transform.position, new Vector2(x, y + 3), fireBallSpeed * Time.deltaTime);
            tempFireballs3.transform.position = Vector3.MoveTowards(tempFireballs3.transform.position, new Vector2(x, y - 3), fireBallSpeed * Time.deltaTime);

            if (tempFireballs1.transform.position.x == x ||
                tempFireballs2.transform.position.x == x ||
                tempFireballs3.transform.position.x == x)
            {
                GameObject.Find("Boss").GetComponent<Animator>().SetBool("Attacking", false);
                explosion1 = Instantiate(explosionPrefab, tempFireballs1.transform.position, tempFireballs1.transform.localRotation);
                 explosion2 = Instantiate(explosionPrefab, tempFireballs2.transform.position, tempFireballs2.transform.localRotation);
                 explosion3 = Instantiate(explosionPrefab, tempFireballs3.transform.position, tempFireballs3.transform.localRotation);
                fxGame.PlayOneShot(explosionAudio, 1);

                Destroy(tempFireballs1);
                Destroy(tempFireballs2);
                Destroy(tempFireballs3);

                StartCoroutine("DestroyExplosion");

                timeAttack = true;
            }

        }
    }

    IEnumerator StopAttack()
    {
        GameObject.Find("Boss").GetComponent<Animator>().SetBool("Sleeping", true);
        enemie.position = Vector3.MoveTowards(enemie.position, movementPoints[0].position, speed * Time.deltaTime);
        fireCircleObj.SetActive(false);
        bossStandby = true;
        Destroy(tempFireballs1);
        Destroy(tempFireballs2);
        Destroy(tempFireballs3);
        Destroy(explosion1);
        Destroy(explosion2);
        Destroy(explosion3);
        yield return new WaitForSeconds(10f);
        GameObject.Find("Boss").GetComponent<Animator>().SetBool("Sleeping", false);
        fireCircleObj.SetActive(true);
        bossStandby = false;
        timeAttack = true;
    }

    IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(explosion1);
        Destroy(explosion2);
        Destroy(explosion3);
    }

    void Cinematic()
    {
        float x = 19;
        float y = -1.39f;
        float z = -0.450f;

        if (cinematic && timeExecutedCinematic < 1)
        {
            bossObjForCustcene.transform.position = Vector3.MoveTowards(bossObjForCustcene.transform.position, new Vector3(x,y,z), speed * Time.deltaTime);
            canvas.SetActive(false);
            GameObject.Find("Player").GetComponent<Animator>().SetBool("Running", false);
            GameObject.Find("Player").GetComponent<Animator>().SetBool("Jump", false);
            GameObject.Find("Player").GetComponent<Animator>().SetBool("Walking", false);
            player.GetComponent<Player>().enabled = false;
            BarraCinematic1.SetActive(true);
            BarraCinematic2.SetActive(true);



            if (bossObjForCustcene.transform.position.x == x &&
            bossObjForCustcene.transform.position.y == y &&
            bossObjForCustcene.transform.position.z == z &&
            taunting == true)
            {

                StartCoroutine("executarAnimacaoCinematic");
                
            }

            if (taunting == false)
            {
                timeExecutedCinematic = 1;
                timeAttack = true;
                canMove = true;
                canvas.SetActive(true);
                fxGame.Play();
                player.GetComponent<Player>().enabled = true;
                BarraCinematic1.SetActive(false);
                BarraCinematic2.SetActive(false);
                powerUp1.SetActive(true);
                powerUp2.SetActive(true);
            }

        }
    }


    IEnumerator executarAnimacaoCinematic()
    {
        taunting = true;
        GameObject.Find("Boss").GetComponent<Animator>().SetBool("Taunt", true);
        yield return new WaitForSeconds(4f);
        taunting = false;
        GameObject.Find("Boss").GetComponent<Animator>().SetBool("Taunt", false);
    }

}
