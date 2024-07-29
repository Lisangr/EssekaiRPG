using UnityEngine;

public class PanelsShower : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject characterPanel;
    private void Start()
    {
        inventoryPanel.SetActive(false);
        characterPanel.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            // ���� ������ ��������� ������, �� ���������� ��
            if (!inventoryPanel.activeInHierarchy)
            {
                characterPanel.SetActive(true);
            }
            // ���� ������ ��������� ��������, �� �������� ��
            else
            {
                characterPanel.SetActive(false);
            }
        }
    }
}
