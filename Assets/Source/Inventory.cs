using DungeonCrawl.Actors.Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

    public void SetInventory(List<Item> itemsList)
    {
        _inventory = itemsList;
    }

    public List<Item> GetInventory()
    {
        return _inventory;
    }

    public void DisplayInventory()
    {
        if (IsInventoryVisible == false)
        {
            UpdateInventoryNumbers();

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

    public void UpdateInventoryNumbers()
    {
        int swordNumber = GetItemNumbers()[0];
        int axesNumber = GetItemNumbers()[1];
        int keysNumber = GetItemNumbers()[2];
        int potionsNumber = GetItemNumbers()[3];

        GameObject.Find("SwordsNumber").GetComponent<Text>().text = "" + swordNumber;
        GameObject.Find("AxesNumber").GetComponent<Text>().text = "" + axesNumber;
        GameObject.Find("KeysNumber").GetComponent<Text>().text = "" + keysNumber;
        GameObject.Find("PotionsNumber").GetComponent<Text>().text = "" + potionsNumber;
    }

    private int[] GetItemNumbers()
    {
        int swordNumber = _inventory.Count(n => n is Sword);
        int axesNumber = _inventory.Count(n => n is Axe);
        int keysNumber = _inventory.Count(n => n is Key);
        int potionsNumber = _inventory.Count(n => n is HealthPotion);

        return new int[] { swordNumber, axesNumber, keysNumber, potionsNumber};
    }

    public void RemoveItem(Item item)
    {
        _inventory.Remove(item);
    }

    public Item SelectItemByType(ItemType itemType)
    {
        return _inventory.FirstOrDefault(item => item.Type == itemType); 
    }
}
