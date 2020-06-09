using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    float rotateSpeed = 40;
    public Transform fireCircle;
    public Transform[] movementPoints;
    public int idTarget;
    public Transform enemie;
    float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireCircle.transform.Rotate(0, 0, Time.deltaTime * rotateSpeed);
        Movimentação();
    }

    void Movimentação()
    {
        enemie.position = Vector3.MoveTowards(enemie.position, movementPoints[idTarget].position, speed * Time.deltaTime);

        if (enemie.position == movementPoints[idTarget].position)
        {
            idTarget += 1; // 1
            if (idTarget == movementPoints.Length)
            {
                idTarget = 0;
            }
        }
    }
}
