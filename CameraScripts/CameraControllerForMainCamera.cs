using UnityEngine;
using YG;

public class CameraControllerForMainCamera : MonoBehaviour
{
    public Transform player; // Цель, вокруг которой будет вращаться камера (например, персонаж)
    //public Joystick rotateJoystick;
    public float distance = 5.0f; // Расстояние от камеры до цели
    public float xSpeed = 120.0f; // Скорость вращения по оси X
    public float ySpeed = 120.0f; // Скорость вращения по оси Y

    public float yMinLimit = -20f; // Минимальный угол вращения по оси Y
    public float yMaxLimit = 80f; // Максимальный угол вращения по оси Y

    private float x = 0.0f;
    private float y = 0.0f;
    //internal static Camera main;

    //public float rotationSpeed = 5f; // Скорость вращения камеры
    //public GameObject touchArea; // Пустой GameObject на канвасе для зоны свайпа

    //private Vector2 touchStart;
    //private bool isDragging = false;
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Скрыть курсор и зафиксировать его в центре экрана
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
            if (Input.GetMouseButton(1)) // Проверка нажатия правой кнопки мыши
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + player.position;

            transform.rotation = rotation;
            transform.position = position;

            // Обновление направления взгляда игрока
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

            // Установка поворота
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            transform.rotation = rotation;

            // Расчет позиции на основе нового поворота
            Vector3 position = player.position + transform.forward * -distance;

            // Установка позиции
            transform.position = position;

            // Установка поворота для player (необязательно)
            player.rotation = Quaternion.Euler(0, x, 0);
        }
    }
    void RotateWithTouchArea()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Проверяем, находится ли касание в пределах touchArea
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
                            touchStart = touch.position; // Обновляем touchStart для плавного движения

                            // Обновляем целевые углы вращения
                            x += delta.x * xSpeed * Time.deltaTime;
                            y -= delta.y * ySpeed * Time.deltaTime;

                            y = ClampAngle(y, yMinLimit, yMaxLimit);

                            Quaternion rotation = Quaternion.Euler(y, x, 0);
                            transform.rotation = rotation;

                            Vector3 position = player.position + transform.forward * -distance;
                            transform.position = position;

                            // Плавное вращение игрока в направлении камеры
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
