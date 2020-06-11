using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieDeath : MonoBehaviour
{
    public float enemieLife;
    public float enemieMaxLife;
    public GameObject Player;
    public GameObject Enemie;
    public float ExpEnemie;
    public bool EnemieAlive = true;
    private float weaponDamage;
    public GameObject hitPrefab;
    public GameObject coinPrefab;
    public AudioSource fxGame;
    public AudioClip fxDeath;
    public GameObject myEnemie;
   

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
            Player.GetComponent<Player>().ActualExp = Player.GetComponent<Player>().ActualExp + ExpEnemie;
            GameObject tempEnemieDie = Instantiate(hitPrefab, Enemie.transform.position, Enemie.transform.localRotation);
            fxGame.PlayOneShot(fxDeath);
            Destroy(tempEnemieDie, 10f);
            GameObject coin = Instantiate(coinPrefab, Enemie.transform.position, Enemie.transform.localRotation);
            coin.AddComponent<AmountCoinsDrop>();
            switch (Enemie.name)
            {
                case "Slime":
                    coin.GetComponent<AmountCoinsDrop>().amountCoins = 30;
                    break;
                case "Minotaur":
                    coin.GetComponent<AmountCoinsDrop>().amountCoins = 60;
                    break;
            }
            
            Destroy(coin, 20f);     
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "PlayerAttack")
        {
            myEnemie = this.gameObject;
            weaponDamage = Player.GetComponent<Player>().WeaponDamage;
            enemieLife = enemieLife - weaponDamage;
            Player.GetComponent<Player>().Enemie = myEnemie;
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
