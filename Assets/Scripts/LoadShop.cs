using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class LoadShop : MonoBehaviour
{
    public static LoadShop Instance { get; private set; }

    public GameObject Shop;
    public bool canOpenShop;
    private int playerMoney;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Shop.SetActive(false);
        canOpenShop = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerMoney);
        loadPlayerMoney();
        openShop();
    }

    void openShop()
    {
        try
        {
            if (canOpenShop == true)
            {
                Shop.SetActive(true);
            }
            else
            {
                Shop.SetActive(false);
            }
        }
        catch (Exception e)
        {

        }
    }

    public void closeShop()
    {
        Shop.SetActive(false);
        canOpenShop = false;
    }

    public void buyItem(string id)
    {
        
            switch (id)
            {
                case "Apple":
                if (playerMoney >= 10 && playerMoney != 0)
                {
                    TextMeshProUGUI text = GameObject.Find("TextApple").GetComponent<TextMeshProUGUI>();
                    text.text = (int.Parse(text.text) - 1).ToString();
                    GameObject.Find("Player").GetComponent<Player>().inventory.AddItem(new Item { itemType = Item.ItemType.Apple, amount = 1 });
                    decreasePlayerMoney(10);
                }
                    break;
                case "PotionHP":
                if (playerMoney >= 25 && playerMoney != 0 && playerMoney > 0)
                {
                    TextMeshProUGUI textHP = GameObject.Find("TextPotionHP").GetComponent<TextMeshProUGUI>();
                    textHP.text = (int.Parse(textHP.text) - 1).ToString();
                    GameObject.Find("Player").GetComponent<Player>().inventory.AddItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                    decreasePlayerMoney(25);
                }
                    break;
                case "PotionMP":
                if (playerMoney >= 25 && playerMoney != 0 && playerMoney > 0)
                {
                    TextMeshProUGUI textMP = GameObject.Find("TextPotionMP").GetComponent<TextMeshProUGUI>();
                    textMP.text = (int.Parse(textMP.text) - 1).ToString();
                    GameObject.Find("Player").GetComponent<Player>().inventory.AddItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                    decreasePlayerMoney(25);
                }
                    break;
            }
        
        
    }
    
    void loadPlayerMoney()
    {
        try
        {
            List<Item> itemList = GameObject.Find("Player").GetComponent<Player>().inventory.GetItemList();
            foreach (Item item in itemList)
            {
                if (item.itemType.ToString().Equals("Coin"))
                {
                    playerMoney = item.amount;
                }
            }
        }catch(Exception e)
        {

        }
    }
    
    void decreasePlayerMoney(int amount)
    {
        try
        {
                if(playerMoney - amount <= 0)
                {
                    playerMoney = 0;
                }

                List<Item> itemList = GameObject.Find("Player").GetComponent<Player>().inventory.GetItemList();
                foreach (Item item in itemList)
                {
                if (item.itemType.ToString().Equals("Coin"))
                {
                    GameObject.Find("Player").GetComponent<Player>().inventory.RemoveItem(new Item { itemType = Item.ItemType.Coin, amount = amount });
                }
                }
        }catch(Exception e)
        {

        }
    }
    
}
