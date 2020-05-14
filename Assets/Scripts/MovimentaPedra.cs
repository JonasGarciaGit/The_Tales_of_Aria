using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaPedra : MonoBehaviour
{

    public Transform obstaculo;
    public SpriteRenderer pedra;
    public Transform[] ponto;
    public float speed;
    public int idTarget;
    // Start is called before the first frame update
    void Start()
    {
        pedra = obstaculo.gameObject.GetComponent<SpriteRenderer>();
        obstaculo.position = ponto[0].position;
        idTarget = 1;
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
