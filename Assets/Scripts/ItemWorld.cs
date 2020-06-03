using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;
    private static Player player;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();

    }

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        
        Vector3 randomDir;
        if (player.facingRight == false)
        {
            randomDir.x = 3;
        }
        else
        {
            randomDir.x = -3;
        }

        randomDir.y = 0;
        randomDir.z = 0;
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,0),ForceMode2D.Impulse);
        return itemWorld;
    }

    public void  SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if(item.amount > 1)
        {
            textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            textMeshPro.SetText("");
        }
        
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
