using UnityEngine;

public class RotateCharacterForCustomizationMenu : MonoBehaviour
{
    public static bool isDragging = false;
    private Vector3 initialMousePosition;
    private float rotationSpeed = 100.0f;

    void Update()
    {
        // Захват начальной позиции мыши при нажатии левой кнопки
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            initialMousePosition = Input.mousePosition;
        }

        // Отпускание левой кнопки мыши
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Выполнение поворота при протягивании мыши
        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float deltaX = currentMousePosition.x - initialMousePosition.x;

            // Поворот объекта вокруг оси Y
            transform.Rotate(Vector3.up, -deltaX * rotationSpeed * Time.deltaTime);

            // Обновление начальной позиции мыши для непрерывного поворота
            initialMousePosition = currentMousePosition;
        }
    }
}
