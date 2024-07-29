using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class AmmoSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image icon;
    public string itemName;
    public int itemQuantity;

    private Item item;
    private Vector3 originalPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private InventoryUI inventoryUI;
    private GameObject draggingIcon;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        inventoryUI = FindObjectOfType<InventoryUI>();
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
            Debug.Log($"Item '{itemName}' added to ammo slot with quantity {itemQuantity}.");
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
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Custom logic for clicking on ammo slot if needed
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

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        bool itemMoved = false;
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
                    inventoryUI.inventory.inventoryWeight.AddWeight(item.itemWeight);
                    ClearSlot();
                    inventoryUI.UpdateUI();
                    Debug.Log("Item moved back to inventory slot.");
                    itemMoved = true;
                    break;
                }
            }
        }

        if (!itemMoved)
        {
            transform.position = originalPosition;
            transform.SetParent(originalParent);
        }
    }
}
