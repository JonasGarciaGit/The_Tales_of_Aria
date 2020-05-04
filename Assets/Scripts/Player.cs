using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D playerRigidBody;
    public float speed;
    private float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        MovePlayer(horizontal);
    }

    void MovePlayer(float move){
        playerRigidBody.velocity = new Vector2(move * speed, playerRigidBody.velocity.y);
    }

}
