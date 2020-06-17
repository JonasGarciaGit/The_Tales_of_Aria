using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBool : MonoBehaviour
{

    public bool cinematic = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            cinematic = true;
        }
    }
}
