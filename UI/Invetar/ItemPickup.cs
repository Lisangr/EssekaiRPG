using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    [HideInInspector] public string itemName;
    [HideInInspector] public int itemQuantity = 1;
    [HideInInspector] public string uniqueID;
    public static Dictionary<string, int> itemInventory = new Dictionary<string, int>(); // ������� ��� �������� ���������
    private InventoryUI inventoryUIManager;
    private InventoryWeight inventoryWeight;

    private void Awake()
    {
        inventoryUIManager = FindObjectOfType<InventoryUI>(); // ������� InventoryUIManager
        inventoryWeight = FindObjectOfType<InventoryWeight>(); // ������� InventoryWeight
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

            // ������� ������ �� �����
            Destroy(gameObject);
        }
    }

    public void AddItemToInventory()
    {
        if (inventoryWeight.AddWeight(item.itemWeight)) // ��������� ��� ����
        {
            // ���������, ���� �� ������� ��� � ���������
            if (itemInventory.ContainsKey(itemName))
            {
                // ���� ������� ��� ���� � �������, ����������� ��� ����������
                itemInventory[itemName] += itemQuantity;
            }
            else
            {
                // ���� �������� ��� � �������, ��������� ���
                itemInventory.Add(itemName, itemQuantity);
            }

            Debug.Log($"Added item: {itemName} to inventory. Total quantity: {itemInventory[itemName]}");

            // ��������� UI ���������, ���� �� �������
            if (inventoryUIManager != null && inventoryUIManager.gameObject.activeInHierarchy)
            {
                inventoryUIManager.UpdateUI();
            }
        }
    }
}    