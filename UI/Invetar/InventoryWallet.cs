using UnityEngine;
using UnityEngine.UI;

public class InventoryWallet : MonoBehaviour
{
    public Text walletText;
    private Item item;
    public static int currentMoney; // Текущие деньги

    private void Update()
    {
        walletText.text = "Всего денег " + currentMoney.ToString();
    }

    // Метод для удаления денег при покупке
    public void RemoveMoney(int buyPrice)
    {
        // Проверка, достаточно ли денег для покупки
        if (currentMoney >= buyPrice)
        {
            currentMoney -= buyPrice;
        }
        else
        {
            Debug.Log("---НЕДОСТАТОЧНО ДЕНЕГ---");
        }
    }

    // Метод для добавления денег при продаже
    public void AddMoney(int sellPrice)
    {
        currentMoney += sellPrice;
    }
}
