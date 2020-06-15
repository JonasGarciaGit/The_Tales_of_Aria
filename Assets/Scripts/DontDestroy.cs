using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DontDestroy: MonoBehaviour
{
    public string sceneName;
    private string ActualSceneName;

    private float ActualLife;
    private float ActualMana;
    private float ActualExp;
    private float MaxExp;
    private string Nivel;
    private List<Item> itemList;
    public SpriteRenderer rend;
    public GameObject canvas;
    private bool canUseMagic;
    private bool canEnter;
    private string isPlayer;
    public Font font;
    private bool checkpoint;
    private bool firstTimeAweaking;

    void Start()
    {
        ActualSceneName = Application.loadedLevelName;
        rend = rend.GetComponent<SpriteRenderer>();
        Color c = rend.material.color;
        c.a = 0f;
        rend.material.color = c;
        canEnter = false;
    }

    
    void Update()
    {
        if (Application.loadedLevelName == sceneName)
        {
            GameObject.Find("Player").GetComponent<Player>().ActualLife = ActualLife;
            GameObject.Find("Player").GetComponent<Player>().ActualMana = ActualMana;
            GameObject.Find("Player").GetComponent<Player>().ActualExp = ActualExp;
            GameObject.Find("Player").GetComponent<Player>().Nivel.text = Nivel;
            GameObject.Find("Player").GetComponent<Player>().canUseMagic = canUseMagic;
            GameObject.Find("Player").GetComponent<SalvarPos>().enabled = checkpoint;
            GameObject.Find("Player").GetComponent<Player>().MaxExp = MaxExp;
            GameObject.Find("Player").GetComponent<Player>().firstTimeAweaking = firstTimeAweaking;
            //GameObject.Find("Player").GetComponent<Player>().inventory.SetItemList(itemList);
            foreach (Item item in itemList)
            {
                GameObject.Find("Player").GetComponent<Player>().inventory.AddItem(new Item { itemType = item.itemType, amount = item.amount});
            }
            
            Destroy(gameObject);
             
        }
        try
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                goToNextLevel();
            }
            
        }
        catch(Exception e)
        {

        }
        
    }

    void guardarValores()
    {
        ActualLife = GameObject.Find("Player").GetComponent<Player>().ActualLife;
        ActualMana = GameObject.Find("Player").GetComponent<Player>().ActualMana;
        ActualExp = GameObject.Find("Player").GetComponent<Player>().ActualExp;
        Nivel = GameObject.Find("Player").GetComponent<Player>().Nivel.text;
        itemList = GameObject.Find("Player").GetComponent<Player>().inventory.GetItemList();
        canUseMagic = GameObject.Find("Player").GetComponent<Player>().canUseMagic;
        checkpoint = GameObject.Find("Player").GetComponent<SalvarPos>().isActiveAndEnabled;
        MaxExp = GameObject.Find("Player").GetComponent<Player>().MaxExp;
        firstTimeAweaking = GameObject.Find("Player").GetComponent<Player>().firstTimeAweaking;
    }

    private void goToNextLevel()
    {
        if (isPlayer == "Player" && ActualSceneName == Application.loadedLevelName && canEnter == true)
        {
            guardarValores();
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(MyUI);
            trocarCena();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canEnter = true;
        isPlayer = collision.gameObject.tag;
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<SalvarPos>().SalvarLocalizacao();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayer = null;
        canEnter = false;
    }

    private void OnGUI()
    {
        if (canEnter == true)
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            GUI.skin.label.fontSize = 10;
            GUI.skin.font = font;
            GUI.color = Color.yellow;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 200, 30), "Pressione 'E' para prosseguir");
        }
    }

    IEnumerator FadeIn()
    {
       // canvas.SetActive(false);
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
