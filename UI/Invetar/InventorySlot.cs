using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image icon;
    public Button removeButton;
    public string itemName;
    public int itemQuantity;
    public Text quantityText;
    public int index;
    public string category;

    private Item item;
    private Vector3 originalPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private InventoryUI inventoryUI;
    private bool isOutsideInventory;
    private Vector3 currentMousePosition;
    private GameObject draggingIcon;
    private InventoryWeight inventoryWeight;
    private InventoryWallet inventoryWallet;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        inventoryWeight = FindObjectOfType<InventoryWeight>();
        inventoryWallet = FindObjectOfType<InventoryWallet>();
        removeButton.onClick.AddListener(OnRemoveButton);

        category = gameObject.name;
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

        item = Resources.Load<Item>($"Items/{itemName}");
        if (item != null)
        {
            icon.sprite = item.icon;
            if (icon.sprite != null)
            {
                icon.enabled = true;
                removeButton.interactable = true;
                quantityText.text = itemQuantity.ToString();
                Debug.Log($"Item '{itemName}' added to inventory slot with quantity {itemQuantity}.");
            }
            else
            {
                Debug.LogWarning($"Icon for item '{itemName}' not found.");
                icon.enabled = false;
                removeButton.interactable = false;
                quantityText.text = "";
            }
        }
        else
        {
            Debug.LogWarning($"Item '{itemName}' not found in Resources/Items.");
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
        item = null;
    }

    public void OnRemoveButton()
    {
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
        inventoryWeight.RemoveWeight(item.itemWeight);
        inventoryUI.UpdateUI();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        // Проверяем, что клик произошел правой кнопкой мыши
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Получаем категорию текущего слота
            string category = gameObject.name;

            // Ищем все объекты с компонентом AmmoSlot
            AmmoSlot[] ammoSlots = FindObjectsOfType<AmmoSlot>();

            // Перемещаем предмет в соответствующий слот
            foreach (AmmoSlot ammoSlot in ammoSlots)
            {
                if (ammoSlot.category == item.category)
                {
                    // Устанавливаем иконку предмета в ammoSlot
                    if (ammoSlot.SetItem(item))
                    {
                        // Очистка текущего слота (по желанию)
                        //item = null;

                        inventoryUI.inventory.AddToAmmoInventory(itemName, itemQuantity);
                        ammoSlot.AddItem(itemName, itemQuantity);
                        ItemPickup.itemInventory.Remove(itemName);
                        ClearSlot();

                        Debug.Log("Item successfully moved to AmmoSlot: " + ammoSlot.gameObject.name);
                        
                        break;
                    }
                    else
                    {
                        Debug.LogWarning("Failed to set item in AmmoSlot: " + ammoSlot.gameObject.name);
                    }
                    break;
                }
            }
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

        if (isOutsideInventory && !TraderPanel.tradePossible)
        {
            transform.position = originalPosition;
            transform.SetParent(originalParent);
            itemQuantity -= 1;
            CreateDroppedItem();
            ClearSlot();
            inventoryUI.UpdateUI();
        }

        if (isOutsideInventory && TraderPanel.tradePossible)
        {
            transform.position = originalPosition;
            transform.SetParent(originalParent);
            itemQuantity -= 1;
            Debug.Log("Предмет ПРОДАН C ПОМОЩЬЮ DRAG & DROP");

            TradingItems();
            ClearSlot();
            inventoryUI.UpdateUI();
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
                    string tempItemName = targetSlot.itemName;
                    int tempItemQuantity = targetSlot.itemQuantity;
                    targetSlot.AddItem(itemName, itemQuantity);
                    AddItem(tempItemName, tempItemQuantity);
                    inventoryWeight.RemoveWeight(item.itemWeight);
                    ItemPickup.itemInventory[itemName] = itemQuantity;
                    itemMoved = true;
                    break;
                }
            }
            else if (result.gameObject.CompareTag("AmmoSlot"))
            {                
                AmmoSlot ammoSlot = result.gameObject.GetComponent<AmmoSlot>();
                if (ammoSlot != null && ammoSlot.category == item.category)
                {
                    inventoryUI.inventory.AddToAmmoInventory(itemName, itemQuantity);
                    ammoSlot.AddItem(itemName, itemQuantity);
                    ItemPickup.itemInventory.Remove(itemName);
                    ClearSlot();
                    //inventoryUI.UpdateAmmoUI();
                    inventoryUI.UpdateUI();
                    Debug.Log("Item moved to ammo slot.");
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

    private void CreateDroppedItem()
    {
        if (!string.IsNullOrEmpty(itemName))
        {
            inventoryWeight.RemoveWeight(item.itemWeight);

            GameObject prefab = Resources.Load<GameObject>($"Items/{itemName}");
            if (prefab != null)
            {
                GameObject droppedItem = Instantiate(prefab);
                droppedItem.transform.position = currentMousePosition;
            }
            else
            {
                Debug.LogWarning($"Prefab for item '{itemName}' not found in Resources/Items.");
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

    private void TradingItems()
    {
        if (!string.IsNullOrEmpty(itemName))
        {
            inventoryWeight.RemoveWeight(item.itemWeight);
            Debug.Log("---===Метод продажи СРАБОТАЛ===---");
            inventoryWallet.AddMoney(item.sellPrice);
            Debug.Log("---===Получено " + item.sellPrice + " денег===---");
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
}
