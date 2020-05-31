using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlteradorDeVisao : MonoBehaviour
{
    public Camera mainCamera;
    public float limitedDown;

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
        if(collision.gameObject.tag == "Player")
        {
            mainCamera.GetComponent<ControllerCamera>().limitedDown = limitedDown;
        }
       
    }
}
