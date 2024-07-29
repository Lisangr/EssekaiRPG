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
            // Если панель инвентаря скрыта, то показываем ее
            if (!inventoryPanel.activeInHierarchy)
            {
                characterPanel.SetActive(true);
            }
            // Если панель инвентаря показана, то скрываем ее
            else
            {
                characterPanel.SetActive(false);
            }
        }
    }
}
