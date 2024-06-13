using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TraderInventorySlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image icon;
    public string itemName;
    public int itemQuantity;
    public Text quantityText;
    public int index; // Индекс текущего слота нужно для очищения InventoryUI

    private Item item;
    private InventoryUI inventoryUIManager;
    private Vector3 originalPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private TraderInventoryUI inventoryUI;
    private GameObject draggingIcon;
    private InventoryWeight inventoryWeight;
    private InventoryWallet inventoryWallet;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        inventoryUI = FindObjectOfType<TraderInventoryUI>();
        inventoryWeight = FindObjectOfType<InventoryWeight>(); // Находим InventoryWeight
    }

    private void Start()
    {
        inventoryUIManager = FindObjectOfType<InventoryUI>();
        inventoryWallet = FindObjectOfType<InventoryWallet>();
    }

    public void AddItem(string newItemName, int quantity)
    {
        itemName = newItemName;
        itemQuantity = quantity;
        icon.sprite = Resources.Load<Sprite>($"Icons/{itemName}");
        if (icon.sprite != null)
        {
            icon.enabled = true;
            quantityText.text = itemQuantity.ToString();
        }
        else
        {
            Debug.LogWarning($"Icon for item '{itemName}' not found in Resources/Icons.");
            icon.enabled = false;
            quantityText.text = "";
        }
    }

    public void SetItem(Item newItem)
    {
        item = newItem;
        itemName = newItem.itemName;
        itemQuantity = newItem.quantity;
        icon.sprite = newItem.icon;
        icon.enabled = true;
        quantityText.text = itemQuantity.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AddItemToPlayerInventory();
    }

    private void AddItemToPlayerInventory()
    {
        if (InventoryWallet.currentMoney >= item.buyPrice)
        {
            inventoryWeight.AddWeight(item.itemWeight);

            inventoryWallet.RemoveMoney(item.buyPrice);
            Debug.Log("---===Потрачено " + item.buyPrice + " денег===---");

            if (ItemPickup.itemInventory.ContainsKey(item.itemName))
            {
                ItemPickup.itemInventory[item.itemName] += 1;
            }
            else
            {
                ItemPickup.itemInventory.Add(item.itemName, item.quantity);
            }

            if (inventoryUIManager != null)
            {
                inventoryUIManager.UpdateUI();
            }
        }
        else
        {
            Debug.Log("А ДЕНЕГ ТО НЕТ... ПЕЧАЛЬ ((((");
        }
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

        if (InventoryWallet.currentMoney >= item.buyPrice)
        {
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<InventorySlot>() != null)
                {
                    InventorySlot targetSlot = result.gameObject.GetComponent<InventorySlot>();

                    if (targetSlot != this)
                    {
                        string tempItemName = targetSlot.itemName;
                        int tempItemQuantity = targetSlot.itemQuantity;
                        targetSlot.AddItem(itemName, itemQuantity);

                        AddItemToPlayerInventory();
                        break;
                    }
                }
            }
        }
        else
        {

        }
    }

    public void ClearSlot()
    {
        itemName = null;
        itemQuantity = 0;
        icon.sprite = null;
        icon.enabled = false;
        quantityText.text = "";
    }    
}