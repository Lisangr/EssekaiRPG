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

    public void InitializeItem()
    {
        itemName = item.itemName;
        uniqueID = Guid.NewGuid().ToString();
        Debug.Log($"Initialized item: {itemName} with ID: {uniqueID}");
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(itemName))
        {
            InitializeItem();
        }
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
        if (inventoryWeight.AddWeight(item.itemWeight)) // добавлено для веса
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

            Debug.Log($"Added item: {itemName} to inventory. Total quantity: {itemInventory[itemName]}");

            // Обновляем UI инвентаря, если он активен
            if (inventoryUIManager != null && inventoryUIManager.gameObject.activeInHierarchy)
            {
                inventoryUIManager.UpdateUI();
            }
        }
    }
}    