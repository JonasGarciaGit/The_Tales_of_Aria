using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropaMaça : MonoBehaviour
{

    public GameObject apple;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            apple.AddComponent<Rigidbody2D>();
        }
    }
}
