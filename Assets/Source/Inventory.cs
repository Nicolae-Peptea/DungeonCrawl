using DungeonCrawl.Actors.Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private List<Item> _inventory;

    public bool IsInventoryVisible { get; set; } = false;

    public Inventory()
    {
        _inventory = new List<Item>();
    }

    public void AddItem(Item item)
    {
        item.Position = (11111, 11111);
        _inventory.Add(item);
    }

    public List<Item> GetInventory()
    {
        return _inventory;
    }

    public void DisplayInventory()
    {
        if (IsInventoryVisible == false)
        {
            int swordNumber = 0;
            int keyNumber = 0;

            foreach (Item item in GetInventory())
            {
                if (item is Sword)
                {
                    swordNumber += 1;
                }
                else if (item is Key)
                {
                    keyNumber += 1;
                }
            }

            GameObject.Find("SwordsNumber").GetComponent<Text>().text = "" + swordNumber;
            GameObject.Find("KeysNumber").GetComponent<Text>().text = "" + keyNumber;

            foreach (var gameObject in GameObject.FindGameObjectsWithTag("inventory"))
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            foreach (var gameObject in GameObject.FindGameObjectsWithTag("inventory"))
            {
                gameObject.transform.localScale = new Vector3(0, 0, 0);
            }
        }

        IsInventoryVisible = !IsInventoryVisible;
    }
}
