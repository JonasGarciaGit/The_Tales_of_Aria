using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestroyObject : MonoBehaviour
{
    public bool destroy;
    public GameObject Enemie;
    public GameObject ExplosionPrefab;
    public Transform FireBallCopyTransform;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
           SpriteRenderer explosion = ExplosionPrefab.GetComponent<SpriteRenderer>();
           explosion.sortingOrder = 5;
        }catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (destroy == true)
        {
            Enemie = null;
            destroy = false;
        }
    }
    private void FixedUpdate()
    {
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.tag == "Enemie")
            {
                if (destroy == false)
                {
                    Enemie = collision.gameObject;
                    Instantiate(ExplosionPrefab, new Vector3(FireBallCopyTransform.position.x, FireBallCopyTransform.position.y, FireBallCopyTransform.position.z),Quaternion.identity);
                    this.gameObject.SetActive(false);
                }
            }
        }catch(Exception e)
        {
            Debug.Log(e);
        }
       
    }

}
