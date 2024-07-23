using UnityEngine;
using UnityEngine.UI;

public class ScaleObject : MonoBehaviour
{
    // ������ �� ��������
    public Slider sliderX;
    public Slider sliderY;
    public Slider sliderZ;

    // ����������� � ������������ �������
    private float minScale = 20f;
    private float maxScale = 40f;
    private float defaultScale = 30f;

    void Start()
    {        
        // ��������� ���������� �������� �������
        transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);

        // ��������� �������� ��������� �� ���������
        sliderX.value = 0.5f;
        sliderY.value = 0.5f;
        sliderZ.value = 0.5f;

        // �������� �� ������� ��������� �������� ���������
        sliderX.onValueChanged.AddListener(ChangeScaleX);
        sliderY.onValueChanged.AddListener(ChangeScaleY);
        sliderZ.onValueChanged.AddListener(ChangeScaleZ);
    }

    // ����� ��� ��������� �������� �� ��� X
    public void ChangeScaleX(float value)
    {
        RotateCharacterForCustomizationMenu.isDragging = false;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Lerp(minScale, maxScale, value);
        transform.localScale = scale;
    }

    // ����� ��� ��������� �������� �� ��� Y
    public void ChangeScaleY(float value)
    {
        RotateCharacterForCustomizationMenu.isDragging = false;
        Vector3 scale = transform.localScale;
        scale.y = Mathf.Lerp(minScale, maxScale, value);
        transform.localScale = scale;
    }

    // ����� ��� ��������� �������� �� ��� Z
    public void ChangeScaleZ(float value)
    {
        RotateCharacterForCustomizationMenu.isDragging = false;
        Vector3 scale = transform.localScale;
        scale.z = Mathf.Lerp(minScale, maxScale, value);
        transform.localScale = scale;
    }
}
