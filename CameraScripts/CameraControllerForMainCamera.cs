using UnityEngine;
using YG;

public class CameraControllerForMainCamera : MonoBehaviour
{
    public Transform player; // ����, ������ ������� ����� ��������� ������ (��������, ��������)
    //public Joystick rotateJoystick;
    public float distance = 5.0f; // ���������� �� ������ �� ����
    public float xSpeed = 120.0f; // �������� �������� �� ��� X
    public float ySpeed = 120.0f; // �������� �������� �� ��� Y

    public float yMinLimit = -20f; // ����������� ���� �������� �� ��� Y
    public float yMaxLimit = 80f; // ������������ ���� �������� �� ��� Y

    private float x = 0.0f;
    private float y = 0.0f;
    //internal static Camera main;

    //public float rotationSpeed = 5f; // �������� �������� ������
    //public GameObject touchArea; // ������ GameObject �� ������� ��� ���� ������

    //private Vector2 touchStart;
    //private bool isDragging = false;
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // ������ ������ � ������������� ��� � ������ ������
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    void LateUpdate()
    {
        RotateWithMouse();
        /*
        if (YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet)
        {
            //RotateWithJoystick();
            //RotateWithTouchArea();
        }
        else
        {
            RotateWithMouse();
        }
        */
    }
    void RotateWithMouse()
    {
        if (player)
        {
            if (Input.GetMouseButton(1)) // �������� ������� ������ ������ ����
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + player.position;

            transform.rotation = rotation;
            transform.position = position;

            // ���������� ����������� ������� ������
            player.rotation = Quaternion.Euler(0, x, 0);
        }
    }/*
    void RotateWithJoystick()
    {
        if (player)
        {
            if (rotateJoystick.Direction.x != 0 && rotateJoystick.Direction.y != 0)
            {
                float inputX = rotateJoystick.Direction.x;
                float inputY = -rotateJoystick.Direction.y;

                x += inputX * xSpeed * Time.deltaTime;
                y -= inputY * ySpeed * Time.deltaTime;

                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }

            // ��������� ��������
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            transform.rotation = rotation;

            // ������ ������� �� ������ ������ ��������
            Vector3 position = player.position + transform.forward * -distance;

            // ��������� �������
            transform.position = position;

            // ��������� �������� ��� player (�������������)
            player.rotation = Quaternion.Euler(0, x, 0);
        }
    }
    void RotateWithTouchArea()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // ���������, ��������� �� ������� � �������� touchArea
            if (RectTransformUtility.RectangleContainsScreenPoint(touchArea.GetComponent<RectTransform>(), touch.position))
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touchStart = touch.position;
                        isDragging = true;
                        break;

                    case TouchPhase.Moved:
                        if (isDragging)
                        {
                            Vector2 delta = touch.position - touchStart;
                            touchStart = touch.position; // ��������� touchStart ��� �������� ��������

                            // ��������� ������� ���� ��������
                            x += delta.x * xSpeed * Time.deltaTime;
                            y -= delta.y * ySpeed * Time.deltaTime;

                            y = ClampAngle(y, yMinLimit, yMaxLimit);

                            Quaternion rotation = Quaternion.Euler(y, x, 0);
                            transform.rotation = rotation;

                            Vector3 position = player.position + transform.forward * -distance;
                            transform.position = position;

                            // ������� �������� ������ � ����������� ������
                            player.rotation = Quaternion.Euler(0, x, 0);
                        }
                        break;

                    case TouchPhase.Ended:
                        isDragging = false;
                        break;
                }
            }
        }
    }*/
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
