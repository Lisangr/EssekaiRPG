using Cinemachine;
using UnityEngine;
using YG;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomSpeed = 10f;
    public float minFOV = 15f;
    public float maxFOV = 90f;

    private void LateUpdate()
    {
        MapForComputers();
        /*
        if (YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet)
        {
            // Масштабирование с помощью жестов на экране телефона
            MapForMobile();
        }
        else
        {
            // Масштабирование с помощью колеса мыши
            MapForComputers();
        }*/
    }

    private void MapForComputers()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            ZoomCamera(scrollInput);
        }
    }

    private void ZoomCamera(float increment)
    {
        float currentFOV = virtualCamera.m_Lens.FieldOfView;
        float newFOV = currentFOV - increment * zoomSpeed;
        virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(newFOV, minFOV, maxFOV);
    }
    /*
    private void MapForMobile()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
            float currentMagnitude = (touch0.position - touch1.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            ZoomCamera(difference * zoomSpeed * 0.1f);
        }
    }*/
}
