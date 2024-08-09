using System.Diagnostics;
using UnityEngine;

public class PanelsShower : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject characterPanel;
    public GameObject skillTreePanel;
    private void Start()
    {
        inventoryPanel.SetActive(false);
        characterPanel.SetActive(false);
        skillTreePanel.SetActive(false);
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
            // ���� ������ ������ ������, �� ���������� ��
            if (!characterPanel.activeInHierarchy)
            {
                characterPanel.SetActive(true);
            }
            // ���� ������ ������ ��������, �� �������� ��
            else
            {
                characterPanel.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            // ���� ������ ����� ������� ������, �� ���������� ��
            if (!skillTreePanel.activeInHierarchy)
            {
                skillTreePanel.SetActive(true);
            }
            // ���� ������ ����� ������� ��������, �� �������� ��
            else
            {
                skillTreePanel.SetActive(false);
            }
        }
    }
}
