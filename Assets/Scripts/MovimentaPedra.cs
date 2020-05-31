using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaPedra : MonoBehaviour
{

   
    public GameObject pedra;
    public Transform[] ponto;
    public float speed;
    public int idTarget = 1;
    public Transform obstaculo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (obstaculo != null)
        {
            obstaculo.position = Vector3.MoveTowards(obstaculo.position, ponto[idTarget].position, speed * Time.deltaTime);

            if(obstaculo.position == ponto[idTarget].position)
            {
                idTarget += 1;
                if(idTarget == ponto.Length)
                {
                    idTarget = 0;
                }
            }
        }
    }
}
