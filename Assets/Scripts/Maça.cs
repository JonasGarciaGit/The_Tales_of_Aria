using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Maça : MonoBehaviour
{

    private bool podeColetar;
    public GameObject apple;
    private Transform myTransformApple;
    private Rigidbody2D myRigidBody;
    public Text AppleAmount;
    public Transform myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myTransformApple = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && podeColetar == true)
        {
            Destroy(this.apple);
            AppleAmount.text = (int.Parse(AppleAmount.text) + 1).ToString(); 
        }
    }
    private void FixedUpdate()
    {
        try
        {
            myRigidBody = GetComponent<Rigidbody2D>();
            if (myRigidBody)
            {
                StartCoroutine("moveApple");
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }


        CollectApple();
    }

    void CollectApple()
    {
        try
        {
            double playerY = Math.Round(myPlayer.transform.position.y);
            double playerX = Math.Round(myPlayer.transform.position.x);
            double appleX = Math.Round(myTransformApple.transform.position.x);
            double appleY = Math.Round(myTransformApple.transform.position.y);

            if (playerY == appleY && playerX == appleX)
            {
                podeColetar = true;
            }
            else
            {
                podeColetar = false;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }


    IEnumerator moveApple()
    {
            myRigidBody.MoveRotation(myRigidBody.rotation + -150.0f * Time.fixedDeltaTime);
            myRigidBody.AddForce(new Vector2(0.5f, 0f));
            yield return new WaitForSeconds(3f);
            myRigidBody.freezeRotation = true;
            myRigidBody.velocity = Vector3.zero;
    }
}
