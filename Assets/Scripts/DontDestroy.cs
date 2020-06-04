using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DontDestroy: MonoBehaviour
{
    public string sceneName;
    private string ActualSceneName;

    private float ActualLife;
    private float ActualMana;
    private float ActualExp;
    private string Nivel;
    private List<Item> itemList;
    public SpriteRenderer rend;
    public GameObject canvas;


    void Start()
    {
        ActualSceneName = Application.loadedLevelName;
        rend = rend.GetComponent<SpriteRenderer>();
        Color c = rend.material.color;
        c.a = 0f;
        rend.material.color = c;
    }

    
    void Update()
    {
        if (Application.loadedLevelName == sceneName)
        {
            GameObject.Find("Player").GetComponent<Player>().ActualLife = ActualLife;
            GameObject.Find("Player").GetComponent<Player>().ActualMana = ActualMana;
            GameObject.Find("Player").GetComponent<Player>().ActualExp = ActualExp;
            GameObject.Find("Player").GetComponent<Player>().Nivel.text = Nivel;
            //GameObject.Find("Player").GetComponent<Player>().inventory.SetItemList(itemList);
            foreach(Item item in itemList)
            {
                GameObject.Find("Player").GetComponent<Player>().inventory.AddItem(new Item { itemType = item.itemType, amount = item.amount});
            }
            
            Destroy(gameObject);
             
        }
    }

    void guardarValores()
    {
        ActualLife = GameObject.Find("Player").GetComponent<Player>().ActualLife;
        ActualMana = GameObject.Find("Player").GetComponent<Player>().ActualMana;
        ActualExp = GameObject.Find("Player").GetComponent<Player>().ActualExp;
        Nivel = GameObject.Find("Player").GetComponent<Player>().Nivel.text;
        itemList = GameObject.Find("Player").GetComponent<Player>().inventory.GetItemList();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && ActualSceneName == Application.loadedLevelName)
        {
            guardarValores();
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(MyUI);
            trocarCena();
        }
    }
    IEnumerator FadeIn()
    {
        canvas.SetActive(false);
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }


    void trocarCena()
    {
        StartCoroutine("FadeIn");    
    }
}
