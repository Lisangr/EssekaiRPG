using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScaleObject : MonoBehaviour
{
    // Ссылки на слайдеры
    public Slider sliderX;
    public Slider sliderY;
    public Slider sliderZ;
    public string serverUrl = "http://localhost/rpg/charactercustomizer.php";

    [SerializeField] private string sceneName = "Summon 1";
    // Минимальный и максимальный масштаб
    private float minScale = 20f;
    private float maxScale = 40f;
    private float defaultScale = 30f;

    private ColorPicker colorPicker;
    private FacialHairGroupHandler facialHairGroupHandler;
    Vector3 scale;
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
    private IEnumerator SendScaleData()
    {
        colorPicker = FindObjectOfType<ColorPicker>().GetComponent<ColorPicker>();
        facialHairGroupHandler = FindObjectOfType<FacialHairGroupHandler>().GetComponent<FacialHairGroupHandler>();

        if (colorPicker == null)
        {
            Debug.LogError("ColorPicker component not found in the scene.");
            yield break;
        }

        string ID = PlayerPrefs.GetString("ID");
        Debug.LogError("I HAVE ID from PlayerPrefs. " + ID);

        if (string.IsNullOrEmpty(ID))
        {
            Debug.LogError("User ID is not set in PlayerPrefs.");
            yield break;
        }

        Vector3 scale = transform.localScale;
        Debug.LogError("Scale values: X=" + scale.x + ", Y=" + scale.y + ", Z=" + scale.z);

        WWWForm form = new WWWForm();
        form.AddField("userdata_id", ID);
        form.AddField("scaleX", scale.x.ToString().Replace(',', '.'));
        form.AddField("scaleY", scale.y.ToString().Replace(',', '.'));
        form.AddField("scaleZ", scale.z.ToString().Replace(',', '.'));

        // Add color fields
        form.AddField("HairSliderR", colorPicker.HairSliderR.value.ToString().Replace(',', '.'));
        form.AddField("HairSliderG", colorPicker.HairSliderG.value.ToString().Replace(',', '.'));
        form.AddField("HairSliderB", colorPicker.HairSliderB.value.ToString().Replace(',', '.'));

        form.AddField("SkinSliderR", colorPicker.SkinSliderR.value.ToString().Replace(',', '.'));
        form.AddField("SkinSliderG", colorPicker.SkinSliderG.value.ToString().Replace(',', '.'));
        form.AddField("SkinSliderB", colorPicker.SkinSliderB.value.ToString().Replace(',', '.'));

        form.AddField("BodyArtSliderR", colorPicker.BodyArtSliderR.value.ToString().Replace(',', '.'));
        form.AddField("BodyArtSliderG", colorPicker.BodyArtSliderG.value.ToString().Replace(',', '.'));
        form.AddField("BodyArtSliderB", colorPicker.BodyArtSliderB.value.ToString().Replace(',', '.'));

        form.AddField("EyesSliderR", colorPicker.EyesSliderR.value.ToString().Replace(',', '.'));
        form.AddField("EyesSliderG", colorPicker.EyesSliderG.value.ToString().Replace(',', '.'));
        form.AddField("EyesSliderB", colorPicker.EyesSliderB.value.ToString().Replace(',', '.'));

        form.AddField("PrimarySliderR", colorPicker.PrimarySliderR.value.ToString().Replace(',', '.'));
        form.AddField("PrimarySliderG", colorPicker.PrimarySliderG.value.ToString().Replace(',', '.'));
        form.AddField("PrimarySliderB", colorPicker.PrimarySliderB.value.ToString().Replace(',', '.'));

        form.AddField("SecondarySliderR", colorPicker.SecondarySliderR.value.ToString().Replace(',', '.'));
        form.AddField("SecondarySliderG", colorPicker.SecondarySliderG.value.ToString().Replace(',', '.'));
        form.AddField("SecondarySliderB", colorPicker.SecondarySliderB.value.ToString().Replace(',', '.'));

        form.AddField("LeatherPrimarySliderR", colorPicker.LeatherPrimarySliderR.value.ToString().Replace(',', '.'));
        form.AddField("LeatherPrimarySliderG", colorPicker.LeatherPrimarySliderG.value.ToString().Replace(',', '.'));
        form.AddField("LeatherPrimarySliderB", colorPicker.LeatherPrimarySliderB.value.ToString().Replace(',', '.'));

        form.AddField("MetalPrimarySliderR", colorPicker.MetalPrimarySliderR.value.ToString().Replace(',', '.'));
        form.AddField("MetalPrimarySliderG", colorPicker.MetalPrimarySliderG.value.ToString().Replace(',', '.'));
        form.AddField("MetalPrimarySliderB", colorPicker.MetalPrimarySliderB.value.ToString().Replace(',', '.'));

        form.AddField("LeatherSecondarySliderR", colorPicker.LeatherSecondarySliderR.value.ToString().Replace(',', '.'));
        form.AddField("LeatherSecondarySliderG", colorPicker.LeatherSecondarySliderG.value.ToString().Replace(',', '.'));
        form.AddField("LeatherSecondarySliderB", colorPicker.LeatherSecondarySliderB.value.ToString().Replace(',', '.'));

        form.AddField("MetalSecondarySliderR", colorPicker.MetalSecondarySliderR.value.ToString().Replace(',', '.'));
        form.AddField("MetalSecondarySliderG", colorPicker.MetalSecondarySliderG.value.ToString().Replace(',', '.'));
        form.AddField("MetalSecondarySliderB", colorPicker.MetalSecondarySliderB.value.ToString().Replace(',', '.'));

        form.AddField("MetalDarkSliderR", colorPicker.MetalDarkSliderR.value.ToString().Replace(',', '.'));
        form.AddField("MetalDarkSliderG", colorPicker.MetalDarkSliderG.value.ToString().Replace(',', '.'));
        form.AddField("MetalDarkSliderB", colorPicker.MetalDarkSliderB.value.ToString().Replace(',', '.'));

        // Add saved elements
        facialHairGroupHandler.AddSavedElementsToForm(form);

        using (UnityWebRequest www = UnityWebRequest.Post(serverUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending data: " + www.error);
            }
            else
            {
                Debug.Log("Scale and color data sent successfully");
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
        }
    }

    public void OnToTheGameClick()
    {
        StartCoroutine(SendScaleData());
    }
}
