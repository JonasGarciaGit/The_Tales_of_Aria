using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorAndShowUpCredits : MonoBehaviour
{

    public GameObject door;
    public GameObject darkAura;
    public GameObject boss;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !boss.activeSelf)
        {
            StartCoroutine("openDoor");
        }    
    }

    IEnumerator openDoor()
    {
        darkAura.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        door.GetComponent<Animator>().SetBool("Opening", true);
        door.AddComponent<DontDestroy>().sceneName = "Creditos";
        
    }

}
