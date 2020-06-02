using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void loadMenuPrincipal()
    {
        SceneManager.LoadScene("Menu_The_Tales_Of_Aria");
    }

}
