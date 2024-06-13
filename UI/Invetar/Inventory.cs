using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public int space = 10;
    public List<Item> allItems; // Список всех возможных предметов
    private InventoryUI inventoryUIManager;
    private InventoryWeight inventoryWeight;
    private void Awake()
    {
        inventoryUIManager = FindObjectOfType<InventoryUI>();
        inventoryWeight = FindObjectOfType<InventoryWeight>(); // Находим InventoryWeight// Находим InventoryUIManager
        InitializeInventory();
    }
    private void InitializeInventory()
    {
        foreach (Item item in allItems)
        {
            if (ItemPickup.itemInventory.ContainsKey(item.itemName))
            {
                ItemPickup.itemInventory[item.itemName]++;
                inventoryWeight.AddWeight(item.itemWeight);
            }
            else if (ItemPickup.itemInventory.Count < space)
            {
                ItemPickup.itemInventory.Add(item.itemName, 1);
                inventoryWeight.AddWeight(item.itemWeight);
            }
        }
    }

    public bool Add(Item item)
    {
        if (ItemPickup.itemInventory.ContainsKey(item.itemName))
        {
            ItemPickup.itemInventory[item.itemName]++;
            inventoryUIManager.UpdateUI();
            return true;
        }
        else if (ItemPickup.itemInventory.Count < space)
        {
            ItemPickup.itemInventory.Add(item.itemName, 1);
            inventoryUIManager.UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough room.");
            return false;
        }
    }

    public void Remove(string itemName)
    {
        if (ItemPickup.itemInventory.ContainsKey(itemName))
        {
            ItemPickup.itemInventory[itemName]--;
            if (ItemPickup.itemInventory[itemName] <= 0)
            {
                ItemPickup.itemInventory.Remove(itemName);
            }
            inventoryUIManager.UpdateUI();
        }
    }

    public Item GetItemByName(string itemName)
    {
        foreach (Item item in allItems)
        {
            if (item.itemName == itemName)
            {
                return item;
            }
        }
        return null;
    }
}