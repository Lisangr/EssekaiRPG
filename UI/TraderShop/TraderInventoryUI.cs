using UnityEngine;

public class TraderInventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public TraderInventory inventory;
    public GameObject inventorySlotPrefab;
    private TraderInventorySlot[] slots;

    void Awake()
    {
        inventory = FindObjectOfType<TraderInventory>();
        slots = itemsParent.GetComponentsInChildren<TraderInventorySlot>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        int i = 0;
        foreach (var item in TraderItemPickup.itemTraderInventory)
        {
            if (i < slots.Length)
            {
                slots[i].index = i; // Установка индекса слота
                slots[i].AddItem(item.Key, item.Value);
                i++;
            }
        }

        // Очищаем оставшиеся слоты
        for (; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
        }
    }

    public string GetSelectedItemName(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length)
        {
            return null;
        }

        var slot = slots[slotIndex];
        if (slot.itemName != null)
        {
            return slot.itemName;
        }

        return null;
    }   
}
