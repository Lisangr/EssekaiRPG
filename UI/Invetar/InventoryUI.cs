using System.Runtime.CompilerServices;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Inventory inventory;
    public GameObject inventorySlotPrefab;
    private InventorySlot[] slots;
    private InventorySlot inventorySlots;
    private AmmoSlot[] ammoSlots;
    private Item item;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        ammoSlots = FindObjectsOfType<AmmoSlot>();
        inventorySlots = gameObject.GetComponent<InventorySlot>();
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
                slots[i].index = i;
                slots[i].AddItem(item.Key, item.Value);
                i++;
            }
        }

        for (; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
        }        
    }

    public void UpdateAmmoUI()
    {
        // Update ammo slots
        foreach (var ammoSlot in ammoSlots)
        {
            //if (item.category == ammoSlot.category && item.category != inventorySlots.category)
            //{
                ammoSlot.ClearSlot();
            //}
        }

        foreach (var item in Inventory.ammoInventory)
        {
            foreach (var ammoSlot in ammoSlots)
            {
                if (ammoSlot.itemName == null)
                {
                    ammoSlot.AddItem(item.Key, item.Value);
                    break;
                }
            }
        }
    }
}