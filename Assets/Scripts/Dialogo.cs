using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogo : MonoBehaviour
{
    [SerializeField]
    private Text nome;
    [SerializeField]
    private Text frase;
    [SerializeField]
    private string nomeDoNpc;
    [SerializeField]
    private string[] frases;
    [SerializeField]
    private GameObject caixaDialogo;
    [SerializeField]
    private Text despedida;
    private int contador = 0;
    private bool podeInteragir = false;
    [SerializeField]
    private GameObject npc;
    // Start is called before the first frame update
    void Start()
    {
        nome.text = nomeDoNpc;
        frase.text = frases[contador];
        caixaDialogo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && podeInteragir == true)
        {
            caixaDialogo.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        frase.text = frases[contador];
    }

    public void continuarConversa()
    {
        if (contador < frases.Length - 1)
        {       
            contador++;
        }
        else
        {
            contador = 0;
            caixaDialogo.SetActive(false);
            podeInteragir = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(npc.tag == "Npc"){
            podeInteragir = true;
        }
        else
        {
            podeInteragir = false;
        }
    }
}
