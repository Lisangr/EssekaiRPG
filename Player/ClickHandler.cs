using TMPro;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // ������ �� ������� TMPro
    public static Transform enemyPosition;
    public static float distance;
    public static string tempID;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // �������� ������� ����� ������ ����
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                textMeshPro.text = clickedObject.name;

                if (Physics.Raycast(transform.position, clickedObject.transform.position - transform.position, out hit))
                {
                    // �������� ���������� �� ����� ������ ���� �� ����� ����������� � �����
                    distance = hit.distance;

                    // ������� ���������� � �������
                    Debug.Log("���������� �� ����: " + distance);
                    if (clickedObject != null && clickedObject.name == "Enemy")
                    {
                        enemyPosition = clickedObject.transform;
                    }

                    // ��������� ���������� ������������� �������
                    ItemPickup itemPickup = clickedObject.GetComponent<ItemPickup>();
                    if (itemPickup != null)
                    {
                        tempID = itemPickup.uniqueID;
                    }
                }
            }
        }
    }
}