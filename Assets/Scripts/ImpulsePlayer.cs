using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulsePlayer : MonoBehaviour
{

    public GameObject Player;
    public bool impulse;
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        impulse = false;
    }

    // Update is called once per frame
    void Update()
    {
        impulsePlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            impulse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        impulse = false;
    }

    void impulsePlayer()
    {
        if(Input.GetKeyDown(KeyCode.E) && impulse == true)
        {
            Rigidbody2D myRigidBody = Player.GetComponent<Rigidbody2D>();
            playerTransform = Player.GetComponent<Transform>();

            Player.GetComponent<Player>().horizontal = 0;
            Player.GetComponent<Player>().speed = 0;
            Player.GetComponent<Player>().playerAnimator.SetBool("Jump", true);
            Player.GetComponent<Player>().playerAnimator.SetBool("IsGrounded", false);

            myRigidBody.AddForce(new Vector2(5,10),ForceMode2D.Impulse);
        }
    }


}
