using UnityEngine;
using UnityEngine.UI;

public class InventoryWallet : MonoBehaviour
{
    public Text walletText;
    private Item item;
    public static int currentMoney; // ������� ������

    private void Update()
    {
        walletText.text = "����� ����� " + currentMoney.ToString();
    }

    // ����� ��� �������� ����� ��� �������
    public void RemoveMoney(int buyPrice)
    {
        // ��������, ���������� �� ����� ��� �������
        if (currentMoney >= buyPrice)
        {
            currentMoney -= buyPrice;
        }
        else
        {
            Debug.Log("---������������ �����---");
        }
    }

    // ����� ��� ���������� ����� ��� �������
    public void AddMoney(int sellPrice)
    {
        currentMoney += sellPrice;
    }
}
