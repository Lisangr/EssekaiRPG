using UnityEngine;
using UnityEngine.UI;

public class InventoryWeight : MonoBehaviour
{
    public Text weightText;
    private int maxWeight = 500; // ����������� ���������� ���
    private int currentWeight; // ������� ���
    private void Update()
    {
        weightText.text = "��� ��������� " + currentWeight.ToString() 
            + " / " + maxWeight.ToString();
    }
    // ����� ��� ���������� ���� ��������
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

    // ����� ��� �������� ���� ��������
    public void RemoveWeight(int weight)
    {
        currentWeight -= weight;
        if (currentWeight <= 0)
        {
            currentWeight = 0;
        }
    }
}
