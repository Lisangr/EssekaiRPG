using UnityEngine;

public class NewSkillPanel : MonoBehaviour
{
    // Переменные для хранения панелей
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    // Переменная для отслеживания текущего состояния
    private int currentPanelIndex = 0;
    private void Start()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
    }
    // Метод, вызываемый при нажатии кнопки
    public void TogglePanels()
    {
        switch (currentPanelIndex)
        {
            case 0:
                // Показать первую панель
                panel1.SetActive(true);
                panel2.SetActive(false);
                panel3.SetActive(false);
                currentPanelIndex = 1;
                break;
            case 1:
                // Показать вторую панель
                panel1.SetActive(true);
                panel2.SetActive(true);
                panel3.SetActive(false);
                currentPanelIndex = 2;
                break;
            case 2:
                // Показать третью панель
                panel1.SetActive(true);
                panel2.SetActive(true);
                panel3.SetActive(true);
                currentPanelIndex = 3;
                break;
            default:
                // Скрыть все панели
                panel1.SetActive(false);
                panel2.SetActive(false);
                panel3.SetActive(false);
                currentPanelIndex = 0;
                break;
        }
    }
}
