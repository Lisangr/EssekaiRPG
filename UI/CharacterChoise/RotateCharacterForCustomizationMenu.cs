using UnityEngine;

public class RotateCharacterForCustomizationMenu : MonoBehaviour
{
    // Ссылка на объект MainField
    public GameObject mainField;

    // Ссылка на камеру
    public Camera camera;

    // Ссылка на объект Prefab
    public GameObject prefab;
    // Скорость вращения объекта
    private float rotationSpeed = 100.0f;

    // Скорость приближения/отдаления камеры
    private float zoomSpeed = 20.0f;

    // Скорость перемещения камеры вверх/вниз
    private float moveSpeed = 50.0f;

    // Внутренние переменные для отслеживания состояния мыши
    private bool isDragging = false;
    private Vector3 lastMousePosition;

    void Update()
    {
        // Проверка нажатия левой кнопки мыши
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(mainField))
            {
                isDragging = true;
                lastMousePosition = Input.mousePosition;
            }
        }

        // Проверка отпускания левой кнопки мыши
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Если удерживается левая кнопка мыши и происходит перемещение
        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 difference = currentMousePosition - lastMousePosition;

            // Вращение объекта вокруг оси Y в зависимости от движения мыши (инвертированное)
            float rotationY = -difference.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationY, 0);

            // Перемещение камеры вверх/вниз в зависимости от движения мыши
            float moveY = -difference.y * moveSpeed * Time.deltaTime;
            camera.transform.position += new Vector3(0, moveY, 0);

            // Обновление последней позиции мыши
            lastMousePosition = currentMousePosition;
        }

        // Обработка ввода прокрутки колесика мыши для приближения/отдаления камеры
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0.0f)
        {
            // Вычисление направления от камеры к объекту Prefab
            Vector3 direction = (prefab.transform.position - camera.transform.position).normalized;
            camera.transform.position += direction * scrollInput * zoomSpeed;
        }
    }

    // Проверка, находится ли указатель мыши над объектом UI
    private bool IsPointerOverUIObject(GameObject targetObject)
    {
        // Получаем RectTransform объекта
        RectTransform rectTransform = targetObject.GetComponent<RectTransform>();

        // Проверка, находится ли указатель мыши над объектом
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, Camera.main);
    }
}
