using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class AmmoSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image icon;
    public string itemName;
    public int itemQuantity;
    public string category;
    public Image itemIcon;

    private Item item;
    private Vector3 originalPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private InventoryUI inventoryUI;
    private GameObject draggingIcon;

    private void Awake()
    {
        category = gameObject.name;
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    public bool SetItem(Item newItem)
    {
        if (itemIcon == null)
        {
            Debug.LogError("itemIcon не установлен в инспекторе для " + gameObject.name);
            return false;
        }

        if (newItem != null)
        {
            if (newItem.icon == null)
            {
                Debug.LogError("Item icon is null for item: " + newItem.itemName);
                return false;
            }
            itemIcon.sprite = newItem.icon;
            itemIcon.enabled = true;
            item = newItem;
            Debug.Log("Item set in AmmoSlot: " + newItem.itemName);
        }
        else
        {
            itemIcon.sprite = null;
            itemIcon.enabled = false;
            item = null;
        }

        return true;
    }

    public void AddItem(string newItemName, int quantity)
    {
        itemName = newItemName;
        itemQuantity = quantity;

        item = Resources.Load<Item>($"Items/{itemName}");
        if (item != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
            Debug.Log($"Item '{itemName}' added to ammo slot {category} with quantity {itemQuantity}.");
        }
        else
        {
            Debug.LogWarning($"Item '{itemName}' not found in Resources/Items.");
            icon.enabled = false;
        }
    }

    public void ClearSlot()
    {
        itemName = null;
        itemQuantity = 0;
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public bool IsSlotEmpty()
    {
        return item == null;
    }

    public Item GetItem()
    {
        return item;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
        canvasGroup.blocksRaycasts = false;

        draggingIcon = new GameObject("Dragging Icon");
        draggingIcon.transform.SetParent(inventoryUI.transform, false);
        draggingIcon.transform.SetAsLastSibling();

        Image draggingIconImage = draggingIcon.AddComponent<Image>();
        draggingIconImage.sprite = icon.sprite;
        draggingIconImage.raycastTarget = false;

        RectTransform draggingIconRect = draggingIcon.GetComponent<RectTransform>();
        draggingIconRect.sizeDelta = new Vector2(50, 50);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggingIcon != null)
        {
            draggingIcon.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (draggingIcon != null)
        {
            Destroy(draggingIcon);
        }
        bool itemMoved = false;
        MoveToInventory();

        if (!itemMoved)
        {
            transform.position = originalPosition;
            transform.SetParent(originalParent);
        }
    }

    private void MoveToInventory()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<InventorySlot>() != null)
            {
                InventorySlot targetSlot = result.gameObject.GetComponent<InventorySlot>();

                if (targetSlot != null)
                {
                    targetSlot.AddItem(itemName, itemQuantity);
                    if (Inventory.ammoInventory.ContainsKey(itemName))
                    {
                        Inventory.ammoInventory[itemName] -= itemQuantity;
                        if (Inventory.ammoInventory[itemName] <= 0)
                        {
                            Inventory.ammoInventory.Remove(itemName);
                        }
                    }
                    if (ItemPickup.itemInventory.ContainsKey(itemName))
                    {
                        ItemPickup.itemInventory[itemName] += itemQuantity;
                    }
                    else
                    {
                        ItemPickup.itemInventory.Add(itemName, itemQuantity);
                    }
                    ClearSlot();
                    inventoryUI.UpdateUI();
                    //inventoryUI.UpdateAmmoUI();
                    Debug.Log("Item moved back to inventory slot.");
                    break;
                }
            }
        }
    }
}