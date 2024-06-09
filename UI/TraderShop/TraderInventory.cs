using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TraderInventory : MonoBehaviour
{
    public int space = 10;
    public List<Item> allItems; // Список всех возможных предметов
    public List<Image> inventoryButtons; // Список кнопок для предметов

    private void Awake()
    {
        InitializeInventory();
        AssignItemsToButtons();
    }

    private void InitializeInventory()
    {
        foreach (Item item in allItems)
        {
            if (TraderItemPickup.itemTraderInventory.ContainsKey(item.itemName))
            {
                TraderItemPickup.itemTraderInventory[item.itemName]++;
            }
            else if (TraderItemPickup.itemTraderInventory.Count < space)
            {
                TraderItemPickup.itemTraderInventory.Add(item.itemName, 1);
            }
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

    private void AssignItemsToButtons()
    {
        for (int i = 0; i < inventoryButtons.Count && i < allItems.Count; i++)
        {
            TraderInventorySlot buttonHandler = inventoryButtons[i].GetComponent<TraderInventorySlot>();
            if (buttonHandler != null)
            {
                buttonHandler.SetItem(allItems[i]);
            }
        }
    }
    /*
    public int space = 10;
    public List<Item> allItems; // Список всех возможных предметов
    public List<Image> inventoryButtons; // Список кнопок для предметов

    private void Awake()
    {
            InitializeInventory();
            AssignItemsToButtons();        
    }

    private void InitializeInventory()
    {
        foreach (Item item in allItems)
        {
            if (TraderItemPickup.itemTraderInventory.ContainsKey(item.itemName))
            {
                TraderItemPickup.itemTraderInventory[item.itemName]++;
            }
            else if (TraderItemPickup.itemTraderInventory.Count < space)
            {
                TraderItemPickup.itemTraderInventory.Add(item.itemName, 1);
            }
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

    private void AssignItemsToButtons()
    {
        for (int i = 0; i < inventoryButtons.Count && i < allItems.Count; i++)
        {//Buy Script тут убрали и поставили TraderInventorySlot
            TraderInventorySlot buttonHandler = inventoryButtons[i].GetComponent<TraderInventorySlot>();
            if (buttonHandler != null)
            {
                buttonHandler.SetItem(allItems[i]);
                
            }
        }
    }*/
}
