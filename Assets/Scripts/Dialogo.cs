using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogo : MonoBehaviour
{
    [SerializeField]
    public Text nome;
    [SerializeField]
    public Text frase;
    [SerializeField]
    public string nomeDoNpc;
    [SerializeField]
    public string[] frases;
    [SerializeField]
    public GameObject caixaDialogo;
    [SerializeField]
    public Text despedida;
    public int contador = 0;
    public bool podeInteragir = false;
    [SerializeField]
    public GameObject npc;
    public LoadShop loadshop;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        nome.text = nomeDoNpc;
        frase.text = frases[contador];
        caixaDialogo.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
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
            if(npc.gameObject.tag == "Merchant")
            {
                loadshop.canOpenShop = true;
                loadshop.Shop.SetActive(true);
                player.activeInventory = true;
                player.InventoryCanvas.SetActive(true);
                

            }
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
