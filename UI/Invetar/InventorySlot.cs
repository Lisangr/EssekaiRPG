using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image icon;
    public Button removeButton;
    public string itemName;
    public int itemQuantity;
    public Text quantityText;
    public int index; // Индекс текущего слота нужно для очищения InventoryUI

    private Item item;
    private Vector3 originalPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private InventoryUI inventoryUI;
    private bool isOutsideInventory;
    private Vector3 currentMousePosition;
    private GameObject draggingIcon;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        removeButton.onClick.AddListener(OnRemoveButton);
    }

    private void Update()
    {
        Vector3 screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            currentMousePosition = hit.point;
        }
    }

    public void AddItem(string newItemName, int quantity)
    {
        itemName = newItemName;
        itemQuantity = quantity;
        icon.sprite = Resources.Load<Sprite>($"Icons/{itemName}");
        if (icon.sprite != null)
        {
            icon.enabled = true;
            removeButton.interactable = true;
            quantityText.text = itemQuantity.ToString();
        }
        else
        {
            Debug.LogWarning($"Icon for item '{itemName}' not found in Resources/Icons.");
            icon.enabled = false;
            removeButton.interactable = false;
            quantityText.text = "";
        }
    }

    public void ClearSlot()
    {
        itemName = null;
        itemQuantity = 0;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        quantityText.text = "";
    }

    public void OnRemoveButton()
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        inventory.Remove(itemName);
        ClearSlot();
        inventoryUI.UpdateUI();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRemoveButton();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
        canvasGroup.blocksRaycasts = false;

        draggingIcon = Instantiate(new GameObject("Dragging Icon"), transform.position, transform.rotation);
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

        if (!RectTransformUtility.RectangleContainsScreenPoint(inventoryUI.GetComponent<RectTransform>(), Input.mousePosition))
        {
            isOutsideInventory = true;
        }
        else
        {
            isOutsideInventory = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (draggingIcon != null)
        {
            Destroy(draggingIcon);
        }

        if (isOutsideInventory)
        {
            transform.position = originalPosition;
            transform.SetParent(originalParent);
            itemQuantity -= 1;
            CreateDroppedItem();
            ClearSlot();
            inventoryUI.UpdateUI();
        }
        else
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

                    if (targetSlot != this)
                    {
                        string tempItemName = targetSlot.itemName;
                        int tempItemQuantity = targetSlot.itemQuantity;
                        targetSlot.AddItem(itemName, itemQuantity);
                        AddItem(tempItemName, tempItemQuantity);
                        break;
                    }
                }
            }
        }
    }

    private void CreateDroppedItem()
    {
        if (!string.IsNullOrEmpty(itemName))
        {
            GameObject prefab = Resources.Load<GameObject>($"Items/{itemName}");
            if (prefab != null)
            {
                GameObject droppedItem = Instantiate(prefab);
                droppedItem.transform.position = currentMousePosition;
            }
            else
            {
                Debug.LogWarning($"Prefab for item '{itemName}' not found in Resources/Prefabs.");
            }
        }

        if (ItemPickup.itemInventory.TryGetValue(itemName, out int quantity))
        {
            if (quantity > 1)
            {
                quantity -= 1;
                ItemPickup.itemInventory[itemName] = quantity;
            }
            else if (quantity == 1)
            {
                ItemPickup.itemInventory.Remove(itemName);
            }
        }
    }
}