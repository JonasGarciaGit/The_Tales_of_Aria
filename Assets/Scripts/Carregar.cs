using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Carregar : MonoBehaviour
{
    bool podeInteragir = false;
    public GameObject Jogador;
    public string CenaACareggar;
    
    void Update(){
        if(podeInteragir == true && Input.GetKeyDown(KeyCode.E)){
            Jogador.GetComponent<SalvarPos>().SalvarLocalizacao();
            SceneManager.LoadScene(CenaACareggar);
        }
    }
    void OnTriggerEnter2D(){
        podeInteragir = true;
    }
    void OnTriggerExit2D(){
        podeInteragir = false;
    }

    void OnGUI(){
        if(podeInteragir == true){
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            GUI.skin.label.fontSize = 20;
            GUI.Label(new Rect(Screen.width/2 - 50, Screen.height/2 + 50, 200,30),"Pressione 'E'");
        }
    }
}
