using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderItemPickup : MonoBehaviour
{
    public Item item;
    [HideInInspector] public string itemName;
    [HideInInspector] public int itemQuantity = 1;
    [HideInInspector] public string uniqueID;
    public static Dictionary<string, int> itemTraderInventory = new Dictionary<string, int>(); // Словарь для хранения предметов
    private TraderInventoryUI inventoryUIManager;
    private void Awake()
    {
        inventoryUIManager = FindObjectOfType<TraderInventoryUI>(); // Находим InventoryUIManager
    }
    private void Start()
    {
        itemName = item.itemName;
        
    }

    private void Update()
    {
            // Проверяем, есть ли предмет уже в инвентаре
            if (itemTraderInventory.ContainsKey(itemName))
            {
                // Если предмет уже есть в словаре, увеличиваем его количество
                itemTraderInventory[itemName] += itemQuantity;
            }
            else
            {
                // Если предмета нет в словаре, добавляем его
                itemTraderInventory.Add(itemName, itemQuantity);
            }

            // Обновляем UI инвентаря
            if (inventoryUIManager != null)
            {
                inventoryUIManager.UpdateUI();
            }
    }
}
