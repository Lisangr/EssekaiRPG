using UnityEngine;
using UnityEngine.UI;

public class ScaleObject : MonoBehaviour
{
    // Ссылки на слайдеры
    public Slider sliderX;
    public Slider sliderY;
    public Slider sliderZ;

    // Минимальный и максимальный масштаб
    private float minScale = 20f;
    private float maxScale = 40f;
    private float defaultScale = 30f;

    void Start()
    {        
        // Установка начального масштаба объекта
        transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);

        // Установка значений слайдеров по умолчанию
        sliderX.value = 0.5f;
        sliderY.value = 0.5f;
        sliderZ.value = 0.5f;

        // Подписка на события изменения значений слайдеров
        sliderX.onValueChanged.AddListener(ChangeScaleX);
        sliderY.onValueChanged.AddListener(ChangeScaleY);
        sliderZ.onValueChanged.AddListener(ChangeScaleZ);
    }

    // Метод для изменения масштаба по оси X
    public void ChangeScaleX(float value)
    {
        RotateCharacterForCustomizationMenu.isDragging = false;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Lerp(minScale, maxScale, value);
        transform.localScale = scale;
    }

    // Метод для изменения масштаба по оси Y
    public void ChangeScaleY(float value)
    {
        RotateCharacterForCustomizationMenu.isDragging = false;
        Vector3 scale = transform.localScale;
        scale.y = Mathf.Lerp(minScale, maxScale, value);
        transform.localScale = scale;
    }

    // Метод для изменения масштаба по оси Z
    public void ChangeScaleZ(float value)
    {
        RotateCharacterForCustomizationMenu.isDragging = false;
        Vector3 scale = transform.localScale;
        scale.z = Mathf.Lerp(minScale, maxScale, value);
        transform.localScale = scale;
    }
}
