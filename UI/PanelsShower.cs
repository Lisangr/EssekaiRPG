using UnityEngine;

public class PanelsShower : MonoBehaviour
{
    public GameObject inventoryPanel;

    private void Start()
    {
        inventoryPanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // ���� ������ ��������� ������, �� ���������� ��
            if (!inventoryPanel.activeInHierarchy)
            {
                inventoryPanel.SetActive(true);
            }
            // ���� ������ ��������� ��������, �� �������� ��
            else
            {
                inventoryPanel.SetActive(false);
            }
        }
    }
}
