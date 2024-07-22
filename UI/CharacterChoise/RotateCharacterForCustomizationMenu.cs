using UnityEngine;

public class RotateCharacterForCustomizationMenu : MonoBehaviour
{
    // ������ �� ������ MainField
    public GameObject mainField;

    // ������ �� ������
    public Camera camera;

    // ������ �� ������ Prefab
    public GameObject prefab;
    // �������� �������� �������
    private float rotationSpeed = 100.0f;

    // �������� �����������/��������� ������
    private float zoomSpeed = 20.0f;

    // �������� ����������� ������ �����/����
    private float moveSpeed = 50.0f;

    // ���������� ���������� ��� ������������ ��������� ����
    private bool isDragging = false;
    private Vector3 lastMousePosition;

    void Update()
    {
        // �������� ������� ����� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(mainField))
            {
                isDragging = true;
                lastMousePosition = Input.mousePosition;
            }
        }

        // �������� ���������� ����� ������ ����
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // ���� ������������ ����� ������ ���� � ���������� �����������
        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 difference = currentMousePosition - lastMousePosition;

            // �������� ������� ������ ��� Y � ����������� �� �������� ���� (���������������)
            float rotationY = -difference.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationY, 0);

            // ����������� ������ �����/���� � ����������� �� �������� ����
            float moveY = -difference.y * moveSpeed * Time.deltaTime;
            camera.transform.position += new Vector3(0, moveY, 0);

            // ���������� ��������� ������� ����
            lastMousePosition = currentMousePosition;
        }

        // ��������� ����� ��������� �������� ���� ��� �����������/��������� ������
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0.0f)
        {
            // ���������� ����������� �� ������ � ������� Prefab
            Vector3 direction = (prefab.transform.position - camera.transform.position).normalized;
            camera.transform.position += direction * scrollInput * zoomSpeed;
        }
    }

    // ��������, ��������� �� ��������� ���� ��� �������� UI
    private bool IsPointerOverUIObject(GameObject targetObject)
    {
        // �������� RectTransform �������
        RectTransform rectTransform = targetObject.GetComponent<RectTransform>();

        // ��������, ��������� �� ��������� ���� ��� ��������
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, Camera.main);
    }
}
