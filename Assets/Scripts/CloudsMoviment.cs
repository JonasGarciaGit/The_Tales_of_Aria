using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CloudsMoviment : MonoBehaviour
{
    public Transform cloud;
    public Transform[] position;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        cloud.position = position[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        moverNuvens();
    }

    void moverNuvens()
    {
        cloud.position = Vector3.MoveTowards(cloud.position, position[1].position, speed * Time.deltaTime);

        if (cloud.position == position[1].position)
        {
            cloud.position = position[0].position;
        }
    }
}
