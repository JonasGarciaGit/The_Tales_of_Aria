using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class HitboxScene : MonoBehaviour
{

    public Transform Player;

    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Transform>();    
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player.position = new Vector2(221.435f, -28.191f);
        }
    }
   
}
