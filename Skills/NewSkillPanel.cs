using UnityEngine;

public class NewSkillPanel : MonoBehaviour
{
    // ���������� ��� �������� �������
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    // ���������� ��� ������������ �������� ���������
    private int currentPanelIndex = 0;
    private void Start()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
    }
    // �����, ���������� ��� ������� ������
    public void TogglePanels()
    {
        switch (currentPanelIndex)
        {
            case 0:
                // �������� ������ ������
                panel1.SetActive(true);
                panel2.SetActive(false);
                panel3.SetActive(false);
                currentPanelIndex = 1;
                break;
            case 1:
                // �������� ������ ������
                panel1.SetActive(true);
                panel2.SetActive(true);
                panel3.SetActive(false);
                currentPanelIndex = 2;
                break;
            case 2:
                // �������� ������ ������
                panel1.SetActive(true);
                panel2.SetActive(true);
                panel3.SetActive(true);
                currentPanelIndex = 3;
                break;
            default:
                // ������ ��� ������
                panel1.SetActive(false);
                panel2.SetActive(false);
                panel3.SetActive(false);
                currentPanelIndex = 0;
                break;
        }
    }
}
