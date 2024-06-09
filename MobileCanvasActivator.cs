using UnityEngine;
using YG;

public class MobileCanvasActivator : MonoBehaviour
{
    public GameObject canvas;
    void Start()
    {
        if (YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet)
        {
            canvas.gameObject.SetActive(true);
        }
        else
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
