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

    private void Start()
    {
        itemName = item.itemName;
        uniqueID = Guid.NewGuid().ToString(); // ��������� ����������� ��������������
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
        if (inventoryWeight.AddWeight(item.itemWeight))//��������� ��� ����
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

            // ��������� UI ���������
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
    public static Dictionary<string, int> itemInventory = new Dictionary<string, int>(); // ������� ��� �������� ���������
    private InventoryUI inventoryUIManager;
    private void Awake()
    {
        inventoryUIManager = FindObjectOfType<InventoryUI>(); // ������� InventoryUIManager
    }
    private void Start()
    {
        itemName = item.itemName;
        uniqueID = Guid.NewGuid().ToString(); // ��������� ����������� ��������������
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && (ClickHandler.tempID == uniqueID) && (ClickHandler.distance <= 8f))
        {
            AddItemToInventory();

            // ������� ������ �� �����
            Destroy(gameObject);
        }
    }

    public void AddItemToInventory()
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

        // ��������� UI ���������
        if (inventoryUIManager != null)
        {
            inventoryUIManager.UpdateUI();
        }
    }*/
}    