using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
   // string nomeCenaAtual;
   // public bool haveCheckpoint;

    void Awake()
    {
        //nomeCenaAtual = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
     /*    PlayerPrefs.SetString(nomeCenaAtual, "true");
        if (PlayerPrefs.HasKey(nomeCenaAtual))
        {
            string check = PlayerPrefs.GetString(nomeCenaAtual);
            if(check.Equals("false"))
            {
                haveCheckpoint = false;
            }
            else
            {
                haveCheckpoint = true;
            }
        }

        if (PlayerPrefs.HasKey(nomeCenaAtual + "X") &&
         PlayerPrefs.HasKey(nomeCenaAtual + "Y") &&
         PlayerPrefs.HasKey(nomeCenaAtual + "Z"))
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat(nomeCenaAtual + "X"), PlayerPrefs.GetFloat(nomeCenaAtual + "Y"), PlayerPrefs.GetFloat(nomeCenaAtual + "Z"));
        } */

    }

   /* public void SalvarLocalizacao()
    {
            PlayerPrefs.SetFloat(nomeCenaAtual + "X", transform.position.x);
            PlayerPrefs.SetFloat(nomeCenaAtual + "Y", transform.position.y);
            PlayerPrefs.SetFloat(nomeCenaAtual + "Z", transform.position.z);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            GameObject.Find("Player").GetComponent<SalvarPos>().enabled = true;
        } 
    }
}
