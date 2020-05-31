using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    //Enumerado com os itens do meu jogo
   public enum ItemType
    {
        Sword,
        Apple,
        Coin,
        HealthPotion,
        ManaPotion
    }

    //Tipo do item
    public ItemType itemType;
    //Quantidade do item
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:        return ItemAssets.Instance.swordSprite;
            case ItemType.Apple:        return ItemAssets.Instance.appleSprite;
            case ItemType.Coin:         return ItemAssets.Instance.coinSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
            case ItemType.ManaPotion:   return ItemAssets.Instance.manaPotionSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Coin:
            case ItemType.Apple:
            case ItemType.HealthPotion:
            case ItemType.ManaPotion:
                return true;
            case ItemType.Sword:
                return false;
        }
    }
}
