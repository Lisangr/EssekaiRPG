using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Text;
using System.Collections;

public class Inventory : MonoBehaviour
{
    public int space = 10;
    public List<Item> allItems;

    [SerializeField] string url = "http://localhost/rpg/inventory.php";
    private Item itemSO;
    private InventoryUI inventoryUIManager;
    private InventoryWeight inventoryWeight;
    public static Dictionary<string, int> ammoInventory = new Dictionary<string, int>();
    private string userId;

    private void Awake()
    {
        Debug.Log("Awake called");
        userId = PlayerPrefs.GetString("ID");
        
        Debug.Log("МЫ ЗАШЛИ ПОД ПЕРСОНАЖЕМ " + userId);
        inventoryUIManager = FindObjectOfType<InventoryUI>();
        inventoryWeight = FindObjectOfType<InventoryWeight>();
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        Debug.Log("Initializing Inventory");
        foreach (Item item in allItems)
        {
            if (ItemPickup.itemInventory.ContainsKey(item.itemName))
            {
                ItemPickup.itemInventory[item.itemName]++;
                inventoryWeight.AddWeight(item.itemWeight);
                Debug.Log("Item already in inventory: " + item.itemName);
            }
            else if (ItemPickup.itemInventory.Count < space)
            {
                ItemPickup.itemInventory.Add(item.itemName, 1);
                inventoryWeight.AddWeight(item.itemWeight);
                Debug.Log("Item added to inventory: " + item.itemName);
            }
            else
            {
                Debug.Log("Not enough space to add item: " + item.itemName);
            }
        }
    }

    public bool Add(Item item)
    {
        Debug.Log("Adding item: " + item.itemName);
        if (ItemPickup.itemInventory.ContainsKey(item.itemName))
        {
            ItemPickup.itemInventory[item.itemName]++;
            inventoryUIManager.UpdateUI();
            SendInventoryData(item);
            Debug.Log("Item count increased: " + item.itemName);
            return true;
        }
        else if (ItemPickup.itemInventory.Count < space)
        {
            ItemPickup.itemInventory.Add(item.itemName, 1);
            inventoryUIManager.UpdateUI();
            SendInventoryData(item);
            Debug.Log("New item added: " + item.itemName);
            return true;
        }
        else
        {
            Debug.Log("Not enough room to add item: " + item.itemName);
            return false;
        }
    }

    public void Remove(string itemName)
    {
        Debug.Log("Removing item: " + itemName);
        if (ItemPickup.itemInventory.ContainsKey(itemName))
        {
            int currentQuantity = ItemPickup.itemInventory[itemName];
            ItemPickup.itemInventory[itemName]--;
            if (ItemPickup.itemInventory[itemName] <= 0)
            {
                ItemPickup.itemInventory.Remove(itemName);
                SendInventoryData(null, itemName, isRemove: true);
                Debug.Log("Item removed from inventory: " + itemName);
            }
            else
            {
                SendInventoryData(null, itemName);
                Debug.Log("Item count decreased: " + itemName);
            }
            inventoryUIManager.UpdateUI();
        }
        else
        {
            Debug.Log("Item not found in inventory: " + itemName);
        }
    }


    public void AddToAmmoInventory(string itemName, int quantity)
    {
        Debug.Log("Adding to ammo inventory: " + itemName + ", quantity: " + quantity);
        if (ammoInventory.ContainsKey(itemName))
        {
            ammoInventory[itemName] += quantity;
        }
        else
        {
            ammoInventory.Add(itemName, quantity);
        }
    }

    public Item GetItemByName(string itemName)
    {
        Debug.Log("Getting item by name: " + itemName);
        foreach (Item item in allItems)
        {
            if (item.itemName == itemName)
            {
                Debug.Log("Item found: " + itemName);
                return item;
            }
        }
        Debug.Log("Item not found: " + itemName);
        return null;
    }

    public void SendInventoryData(Item item, string itemName = null, bool isRemove = false)
    {
        Debug.Log("Sending inventory data");

        // Определяем имя предмета
        string name = item != null ? item.itemName : itemName;

        // Проверяем количество предметов в словаре
        int quantity = 0;
        if (ItemPickup.itemInventory.ContainsKey(name))
        {
            quantity = ItemPickup.itemInventory[name];
        }

        // Создаем данные для отправки
        InventoryData inventoryData = new InventoryData
        {
            userId = userId,
            itemId = item != null ? item.dbID : 0,
            itemName = name,
            quantity = quantity,
            isRemove = isRemove
        };

        // Преобразуем данные в JSON и отправляем запрос
        string jsonData = JsonUtility.ToJson(inventoryData);
        Debug.Log("JSON data: " + jsonData);
        StartCoroutine(PostRequest(url, jsonData));
    }


    IEnumerator PostRequest(string url, string jsonData)
    {
        Debug.Log("Sending POST request to: " + url);
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log("Inventory data successfully sent!");
        }
    }
}

[System.Serializable]
public class InventoryData
{
    public string userId;
    public int itemId;
    public string itemName;
    public int quantity;
    public bool isRemove;
}









/*
 * using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public int space = 10;
    public List<Item> allItems;

    private Item itemSO;
    private InventoryUI inventoryUIManager;
    private InventoryWeight inventoryWeight;
    public static Dictionary<string, int> ammoInventory = new Dictionary<string, int>();

    private void Awake()
    {
        inventoryUIManager = FindObjectOfType<InventoryUI>();
        inventoryWeight = FindObjectOfType<InventoryWeight>();
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        foreach (Item item in allItems)
        {
            if (ItemPickup.itemInventory.ContainsKey(item.itemName))
            {
                ItemPickup.itemInventory[item.itemName]++;
                inventoryWeight.AddWeight(item.itemWeight);
            }
            else if (ItemPickup.itemInventory.Count < space)
            {
                ItemPickup.itemInventory.Add(item.itemName, 1);
                inventoryWeight.AddWeight(item.itemWeight);
            }
        }
    }

    public bool Add(Item item)
    {
        if (ItemPickup.itemInventory.ContainsKey(item.itemName))
        {
            ItemPickup.itemInventory[item.itemName]++;
            inventoryUIManager.UpdateUI();
            return true;
        }
        else if (ItemPickup.itemInventory.Count < space)
        {
            ItemPickup.itemInventory.Add(item.itemName, 1);
            inventoryUIManager.UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough room.");
            return false;
        }
    }

    public void Remove(string itemName)
    {
        if (ItemPickup.itemInventory.ContainsKey(itemName))
        {
            ItemPickup.itemInventory[itemName]--;
            if (ItemPickup.itemInventory[itemName] <= 0)
            {
                ItemPickup.itemInventory.Remove(itemName);
            }
            inventoryUIManager.UpdateUI();
        }
    }

    public Item GetItemByName(string itemName)
    {
        foreach (Item item in allItems)
        {
            if (item.itemName == itemName)
            {
                return item;
            }
        }
        return null;
    }

    public void AddToAmmoInventory(string itemName, int quantity)
    {
        if (ammoInventory.ContainsKey(itemName))
        {
            ammoInventory[itemName] += quantity;
        }
        else
        {
            ammoInventory.Add(itemName, quantity);
        }
    }
}
*/