using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FireBallMagic : MonoBehaviour
{
    public GameObject Player;
    private Transform playerTransform;
    public GameObject fireBallPrefab;
    public GameObject fireball;
    public float fireballDamage;
    private bool destroy = false;
    public float ActualMana;
    public GameObject Enemie;
    public AudioSource fxGame;
    public AudioClip fxFireBall;
    public float costMana;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = Player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            
            if (fireball.GetComponent<DestroyObject>().destroy == false)
            {
                Enemie = fireball.GetComponent<DestroyObject>().Enemie;
            }
            if(fireball != null)
            {
                fireball.GetComponent<DestroyObject>().FireBallCopyTransform = fireball.transform;
            }
        }
        catch(Exception e)
        {

        }

    }

    IEnumerator SpellMagic()
    {
        ActualMana = Player.GetComponent<Player>().ActualMana;
        float positionX = playerTransform.position.x;
        float positionY = playerTransform.position.y;
        float positionZ = playerTransform.position.z;

        if (ActualMana >= costMana)
        {
            fireball = Instantiate(fireBallPrefab, new Vector3(positionX + 2, positionY, positionZ), Quaternion.identity);
            fxGame.PlayOneShot(fxFireBall);
            Rigidbody2D fireballRigidBody = fireball.GetComponent<Rigidbody2D>();
            SpriteRenderer fireballSpriteRender = fireball.GetComponent<SpriteRenderer>();
            ActualMana = ActualMana - costMana;
            
            if (Player.GetComponent<Player>().facingRight)
            {
                fireballSpriteRender.flipX = false;
                fireballRigidBody.AddForce(new Vector2(-400, 0));
            }
            else
            {
                fireballSpriteRender.flipX = true;
                fireballRigidBody.AddForce(new Vector2(400, 0));
            }

            yield return new WaitForSeconds(0.5f);
            Destroy(fireball);
            Instantiate(fireball.GetComponent<DestroyObject>().ExplosionPrefab, new Vector3(fireball.transform.position.x, fireball.transform.position.y, fireball.transform.position.z), Quaternion.identity);
        }
    }



}


