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
            // Если панель инвентаря скрыта, то показываем ее
            if (!inventoryPanel.activeInHierarchy)
            {
                inventoryPanel.SetActive(true);
            }
            // Если панель инвентаря показана, то скрываем ее
            else
            {
                inventoryPanel.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // Если панель игрока скрыта, то показываем ее
            if (!characterPanel.activeInHierarchy)
            {
                characterPanel.SetActive(true);
            }
            // Если панель игрока показана, то скрываем ее
            else
            {
                characterPanel.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            // Если панель древа скиллов скрыта, то показываем ее
            if (!skillTreePanel.activeInHierarchy)
            {
                skillTreePanel.SetActive(true);
            }
            // Если панель древа скиллов показана, то скрываем ее
            else
            {
                skillTreePanel.SetActive(false);
            }
        }
    }
}
