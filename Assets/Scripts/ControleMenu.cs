using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ControleMenu : MonoBehaviour
{
    
    public static ControleMenu Instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CarregaCena(string nomeDaCena)
    {
        if(nomeDaCena == "Intro")
        {
            SceneManager.MoveGameObjectToScene(GameObject.Find("IntroMusic"), SceneManager.GetActiveScene());
            SceneManager.MoveGameObjectToScene(GameObject.Find("CameraPrincipal"), SceneManager.GetActiveScene());
        }

        SceneManager.LoadScene(nomeDaCena);
    }

    public void sairJogo()
    {
        Application.Quit();
    }

}
