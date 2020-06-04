using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Conversa : MonoBehaviour
{
    public string nomeDoNpc;
    public string[] frases;
    public GameObject npc;
    public GameObject Dialogo;
    public Font font;
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Dialogo.GetComponent<Dialogo>().podeInteragir == true)
        {
            Dialogo.GetComponent<Dialogo>().caixaDialogo.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.tag == "Player")
            {
                Dialogo.GetComponent<Dialogo>().podeInteragir = true;
                Dialogo.GetComponent<Dialogo>().nomeDoNpc = nomeDoNpc;
                Dialogo.GetComponent<Dialogo>().npc = npc;
                Dialogo.GetComponent<Dialogo>().frases = new string[frases.Length];
                Dialogo.GetComponent<Dialogo>().item = item;
                for (int i = 0; i < frases.Length; i++)
                {
                    Dialogo.GetComponent<Dialogo>().frases[i] = frases[i];
                }
            }
        }
        catch (Exception e)
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Dialogo.GetComponent<Dialogo>().podeInteragir = false;
    }

    private void OnGUI()
    {
        if (Dialogo.GetComponent<Dialogo>().podeInteragir == true)
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            GUI.skin.label.fontSize = 10;
            GUI.skin.font = font;
            GUI.color = Color.yellow;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 200, 30), "Pressione 'E' para falar");
        }
    }
}
