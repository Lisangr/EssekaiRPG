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
    }
}
