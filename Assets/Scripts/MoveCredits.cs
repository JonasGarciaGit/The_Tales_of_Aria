using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCredits : MonoBehaviour
{

    public float tempo;
    public GameObject letras;

    // Start is called before the first frame update
    void Start()
    {
        letras.transform.position = new Vector3(letras.transform.position.x, letras.transform.position.y -600 , letras.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float y = letras.transform.position.y;
        letras.transform.position = new Vector3(letras.transform.position.x, y += Screen.height / tempo * Time.deltaTime, letras.transform.position.z);

        if(letras.transform.position.y > Screen.height *2)
        {
            SceneManager.LoadScene("Menu_The_Tales_Of_Aria");
        }
    }
}
