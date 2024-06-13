using UnityEngine;
using UnityEngine.UI;

public class InventoryWeight : MonoBehaviour
{
    public Text weightText;
    private int maxWeight = 500; // Максимально допустимый вес
    private int currentWeight; // Текущий вес
    private void Update()
    {
        weightText.text = "Вес инвентаря " + currentWeight.ToString() 
            + " / " + maxWeight.ToString();
    }
    // Метод для добавления веса предмета
    public bool AddWeight(int weight)
    {
        if (currentWeight + weight <= maxWeight)
        {
            currentWeight += weight;
            return true;
        }
        else
        {
            Debug.Log("Not enough weight capacity.");
            return false;
        }
    }

    // Метод для удаления веса предмета
    public void RemoveWeight(int weight)
    {
        currentWeight -= weight;
        if (currentWeight <= 0)
        {
            currentWeight = 0;
        }
    }
}
