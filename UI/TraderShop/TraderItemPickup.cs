using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderItemPickup : MonoBehaviour
{
    public Item item;
    [HideInInspector] public string itemName;
    [HideInInspector] public int itemQuantity = 1;
    [HideInInspector] public string uniqueID;
    public static Dictionary<string, int> itemTraderInventory = new Dictionary<string, int>(); // ������� ��� �������� ���������
    private TraderInventoryUI inventoryUIManager;
    private void Awake()
    {
        inventoryUIManager = FindObjectOfType<TraderInventoryUI>(); // ������� InventoryUIManager
    }
    private void Start()
    {
        itemName = item.itemName;
        
    }

    private void Update()
    {
            // ���������, ���� �� ������� ��� � ���������
            if (itemTraderInventory.ContainsKey(itemName))
            {
                // ���� ������� ��� ���� � �������, ����������� ��� ����������
                itemTraderInventory[itemName] += itemQuantity;
            }
            else
            {
                // ���� �������� ��� � �������, ��������� ���
                itemTraderInventory.Add(itemName, itemQuantity);
            }

            // ��������� UI ���������
            if (inventoryUIManager != null)
            {
                inventoryUIManager.UpdateUI();
            }
    }
}
