using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Inventory inventory;
    public GameObject inventorySlotPrefab;
    private InventorySlot[] slots;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        int i = 0;
        foreach (var item in ItemPickup.itemInventory)
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
}