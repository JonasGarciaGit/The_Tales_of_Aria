using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieTeste : MonoBehaviour
{
    public float enemieLife;
    public float enemieMaxLife;
    public GameObject Player;
    public GameObject Enemie;
    public float ExpEnemie;
    public bool EnemieAlive = true;
    private float weaponDamage;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        enemieLife = enemieMaxLife;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        if (enemieLife <= 0)
        {
            EnemieAlive = false;
            Enemie.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            weaponDamage = Player.GetComponent<Player>().WeaponDamage;
            enemieLife = enemieLife - weaponDamage;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "FireBall")
        {
            float fireballDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<FireBallMagic>().fireballDamage;
            enemieLife = enemieLife - fireballDamage;
        }
    }


}
