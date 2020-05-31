using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DropaMaça : MonoBehaviour
{

    public GameObject apple;

 
    // Start is called before the first frame update
    void Start()
    {
        apple = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.transform.tag == "Player")
            {
               apple.AddComponent<Rigidbody2D>();
            }
        }catch(Exception e)
        {
            Debug.Log(e);
        }
        
    }
}
