using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    [HideInInspector] public string itemName;
    [HideInInspector] public int itemQuantity = 1;
    [HideInInspector] public string uniqueID;
    public static Dictionary<string, int> itemInventory = new Dictionary<string, int>(); // Словарь для хранения предметов
    private InventoryUI inventoryUIManager;
    private InventoryWeight inventoryWeight;

    private void Awake()
    {
        inventoryUIManager = FindObjectOfType<InventoryUI>(); // Находим InventoryUIManager
        inventoryWeight = FindObjectOfType<InventoryWeight>(); // Находим InventoryWeight
    }

    private void Start()
    {
        itemName = item.itemName;
        uniqueID = Guid.NewGuid().ToString(); // Генерация уникального идентификатора
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && (ClickHandler.tempID == uniqueID) 
            && (ClickHandler.distance <= 8f))
        {
            AddItemToInventory();

            // Удаляем объект из сцены
            Destroy(gameObject);
        }
    }

    public void AddItemToInventory()
    {
        if (inventoryWeight.AddWeight(item.itemWeight))//добавлено для веса
        {
            // Проверяем, есть ли предмет уже в инвентаре
            if (itemInventory.ContainsKey(itemName))
            {
                // Если предмет уже есть в словаре, увеличиваем его количество
                itemInventory[itemName] += itemQuantity;
            }
            else
            {
                // Если предмета нет в словаре, добавляем его
                itemInventory.Add(itemName, itemQuantity);
            }

            // Обновляем UI инвентаря
            if (inventoryUIManager != null)
            {
                inventoryUIManager.UpdateUI();
            }
        }
    }
    /*
    public Item item;
    [HideInInspector] public string itemName;
    [HideInInspector] public int itemQuantity = 1;
    [HideInInspector] public string uniqueID;
    public static Dictionary<string, int> itemInventory = new Dictionary<string, int>(); // Словарь для хранения предметов
    private InventoryUI inventoryUIManager;
    private void Awake()
    {
        inventoryUIManager = FindObjectOfType<InventoryUI>(); // Находим InventoryUIManager
    }
    private void Start()
    {
        itemName = item.itemName;
        uniqueID = Guid.NewGuid().ToString(); // Генерация уникального идентификатора
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && (ClickHandler.tempID == uniqueID) && (ClickHandler.distance <= 8f))
        {
            AddItemToInventory();

            // Удаляем объект из сцены
            Destroy(gameObject);
        }
    }

    public void AddItemToInventory()
    {
        // Проверяем, есть ли предмет уже в инвентаре
        if (itemInventory.ContainsKey(itemName))
        {
            // Если предмет уже есть в словаре, увеличиваем его количество
            itemInventory[itemName] += itemQuantity;
        }
        else
        {
            // Если предмета нет в словаре, добавляем его
            itemInventory.Add(itemName, itemQuantity);
        }        

        // Обновляем UI инвентаря
        if (inventoryUIManager != null)
        {
            inventoryUIManager.UpdateUI();
        }
    }*/
}    