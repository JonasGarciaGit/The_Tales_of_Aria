using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

//Este script deve estar no seu player e deve ter o nome "SalvarPos"
public class SalvarPos : MonoBehaviour
{
    string nomeCenaAtual;
    public string sceneName;
    public Transform playerPosition;

    void Awake()
    {
        nomeCenaAtual = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
       if(PlayerPrefs.HasKey(nomeCenaAtual + "X") &&
        PlayerPrefs.HasKey(nomeCenaAtual + "Y") &&
        PlayerPrefs.HasKey(nomeCenaAtual + "Z")){
            transform.position = new Vector3(PlayerPrefs.GetFloat(nomeCenaAtual + "X"),PlayerPrefs.GetFloat(nomeCenaAtual + "Y"),PlayerPrefs.GetFloat(nomeCenaAtual + "Z"));
        } 

        
    }



    public void SalvarLocalizacao()
    {

        try
        {
            sceneName = GameObject.Find("LimiteTelaBoss").GetComponent<DontDestroy>().sceneName;
            playerPosition = GameObject.Find("LimiteTelaBoss").GetComponent<ImpulsePlayer>().playerTransform;
        }catch(Exception e)
        {

        }

            if (sceneName.Equals("Boss"))
            {
                PlayerPrefs.SetFloat(nomeCenaAtual + "X", playerPosition.position.x);
                PlayerPrefs.SetFloat(nomeCenaAtual + "Y", playerPosition.position.y);
                PlayerPrefs.SetFloat(nomeCenaAtual + "Z", playerPosition.position.z);
            }
            else
            { 
                PlayerPrefs.SetFloat(nomeCenaAtual + "X", transform.position.x);
                PlayerPrefs.SetFloat(nomeCenaAtual + "Y", transform.position.y);
                PlayerPrefs.SetFloat(nomeCenaAtual + "Z", transform.position.z);
            }


    } 
}
