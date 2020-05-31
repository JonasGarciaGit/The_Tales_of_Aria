using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogo : MonoBehaviour
{
    
    public Text nome;
    public Text frase;
    public string nomeDoNpc;
    public string[] frases;
    public GameObject caixaDialogo;
    public Text despedida;
    public int contador = 0;
    public bool podeInteragir = false;
    public GameObject npc;
    public GameObject shop;
    public LoadShop loadshop;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        frase.text = frases[contador];
        caixaDialogo.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        nome.text = nomeDoNpc;
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

            if(npc.gameObject.tag == "Merchant")
            {
                loadshop.canOpenShop = true;
                shop.SetActive(true);
                player. activeInventory = true;
                player.InventoryCanvas.SetActive(true);
            }
        }
    }

  
}
