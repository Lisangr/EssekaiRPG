using TMPro;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Ссылка на элемент TMPro
    public static Transform enemyPosition;
    public static float distance;
    public static string tempID;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверка нажатия левой кнопки мыши
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                textMeshPro.text = clickedObject.name;

                if (Physics.Raycast(transform.position, clickedObject.transform.position - transform.position, out hit))
                {
                    // Получаем расстояние от точки старта луча до точки пересечения с целью
                    distance = hit.distance;

                    // Выводим расстояние в консоль
                    Debug.Log("Расстояние до цели: " + distance);
                    if (clickedObject != null && clickedObject.name == "Enemy")
                    {
                        enemyPosition = clickedObject.transform;
                    }

                    // Сохраняем уникальный идентификатор объекта
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