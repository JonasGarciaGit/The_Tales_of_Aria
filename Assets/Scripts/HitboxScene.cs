using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitboxScene : MonoBehaviour
{
    public string objectName;
    public GameObject playerPositionSave;

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (objectName == "LimiteTelaMorte")
        {
            if(collision.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (objectName == "LimiteTelaBoss")
        {
            if (collision.gameObject.tag == "Player")
            {
                playerPositionSave.GetComponent<SalvarPos>().SalvarLocalizacao();
                SceneManager.LoadScene("Boss");
            }
        }
    }

}
