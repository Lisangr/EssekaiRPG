using UnityEngine.UI;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    [Header("Hair Color Sliders")]
    public Slider HairSliderR;
    public Slider HairSliderG;
    public Slider HairSliderB;

    [Header("Skin and Stubble Color Sliders")]
    public Slider SkinSliderR;
    public Slider SkinSliderG;
    public Slider SkinSliderB;

    [Header("BodyArt Color Sliders")]
    public Slider BodyArtSliderR;
    public Slider BodyArtSliderG;
    public Slider BodyArtSliderB;

    [Header("Eyes Color Sliders")]
    public Slider EyesSliderR;
    public Slider EyesSliderG;
    public Slider EyesSliderB;

    [Header("Primary Colors")]
    public Slider PrimarySliderR;
    public Slider PrimarySliderG;
    public Slider PrimarySliderB;

    [Header("Secondary Colors")]
    public Slider SecondarySliderR;
    public Slider SecondarySliderG;
    public Slider SecondarySliderB;

    [Header("Leather Primary Color")]
    public Slider LeatherPrimarySliderR;
    public Slider LeatherPrimarySliderG;
    public Slider LeatherPrimarySliderB;

    [Header("Metal Primary Color")]
    public Slider MetalPrimarySliderR;
    public Slider MetalPrimarySliderG;
    public Slider MetalPrimarySliderB;

    [Header("Leather Secondary Color")]
    public Slider LeatherSecondarySliderR;
    public Slider LeatherSecondarySliderG;
    public Slider LeatherSecondarySliderB;

    [Header("Metal Secondary Color")]
    public Slider MetalSecondarySliderR;
    public Slider MetalSecondarySliderG;
    public Slider MetalSecondarySliderB;

    [Header("Metal Dark Color")]
    public Slider MetalDarkSliderR;
    public Slider MetalDarkSliderG;
    public Slider MetalDarkSliderB;

    public Material material;

    private Color colorHair;
    private Color colorSkin;
    private Color colorBodyArt;
    private Color colorEyes;
    private Color colorStubble;
    private Color colorPrimary;
    private Color colorSecondary;
    private Color colorLeatherPrimary;
    private Color colorMetalPrimary;
    private Color colorLeatherSecondary;
    private Color colorMetalSecondary;
    private Color colorMetalDark;

    void Start()
    {
        InitializeSliders();
        UpdateColors();
    }

    void Update()
    {
        UpdateColors();
    }

    private void InitializeSliders()
    {
        HairSliderR.value = 0.4f;
        HairSliderG.value = 0.26f;
        HairSliderB.value = 0.13f;

        SkinSliderR.value = 1.0f;
        SkinSliderG.value = 0.8f;
        SkinSliderB.value = 0.6f;

        BodyArtSliderR.value = 0.0f;
        BodyArtSliderG.value = 0.0f;
        BodyArtSliderB.value = 0.0f;

        EyesSliderR.value = 0.2f;
        EyesSliderG.value = 0.4f;
        EyesSliderB.value = 0.8f;

        PrimarySliderR.value = 0.5f;
        PrimarySliderG.value = 0.5f;
        PrimarySliderB.value = 0.5f;

        SecondarySliderR.value = 0.5f;
        SecondarySliderG.value = 0.5f;
        SecondarySliderB.value = 0.5f;

        LeatherPrimarySliderR.value = 0.5f;
        LeatherPrimarySliderG.value = 0.3f;
        LeatherPrimarySliderB.value = 0.2f;

        MetalPrimarySliderR.value = 0.6f;
        MetalPrimarySliderG.value = 0.6f;
        MetalPrimarySliderB.value = 0.6f;

        LeatherSecondarySliderR.value = 0.4f;
        LeatherSecondarySliderG.value = 0.2f;
        LeatherSecondarySliderB.value = 0.1f;

        MetalSecondarySliderR.value = 0.5f;
        MetalSecondarySliderG.value = 0.5f;
        MetalSecondarySliderB.value = 0.5f;

        MetalDarkSliderR.value = 0.2f;
        MetalDarkSliderG.value = 0.2f;
        MetalDarkSliderB.value = 0.2f;
    }

    public void UpdateColors()
    {
        colorHair = new Color(HairSliderR.value, HairSliderG.value, HairSliderB.value);
        colorSkin = new Color(SkinSliderR.value, SkinSliderG.value, SkinSliderB.value);
        colorBodyArt = new Color(BodyArtSliderR.value, BodyArtSliderG.value, BodyArtSliderB.value);
        colorEyes = new Color(EyesSliderR.value, EyesSliderG.value, EyesSliderB.value);
        colorStubble = colorSkin;

        colorPrimary = new Color(PrimarySliderR.value, PrimarySliderG.value, PrimarySliderB.value);
        colorSecondary = new Color(SecondarySliderR.value, SecondarySliderG.value, SecondarySliderB.value);
        colorLeatherPrimary = new Color(LeatherPrimarySliderR.value, LeatherPrimarySliderG.value, LeatherPrimarySliderB.value);
        colorMetalPrimary = new Color(MetalPrimarySliderR.value, MetalPrimarySliderG.value, MetalPrimarySliderB.value);
        colorLeatherSecondary = new Color(LeatherSecondarySliderR.value, LeatherSecondarySliderG.value, LeatherSecondarySliderB.value);
        colorMetalSecondary = new Color(MetalSecondarySliderR.value, MetalSecondarySliderG.value, MetalSecondarySliderB.value);
        colorMetalDark = new Color(MetalDarkSliderR.value, MetalDarkSliderG.value, MetalDarkSliderB.value);

        ApplyColors();
    }

    private void ApplyColors()
    {
        material.SetColor("_Color_Hair", colorHair);
        material.SetColor("_Color_Skin", colorSkin);
        material.SetColor("_Color_Scar", colorBodyArt);
        material.SetColor("_Color_Eyes", colorEyes);
        material.SetColor("_Color_Stubble", colorStubble);

        material.SetColor("_Color_Primary", colorPrimary);
        material.SetColor("_Color_Secondary", colorSecondary);
        material.SetColor("_Color_Leather_Primary", colorLeatherPrimary);
        material.SetColor("_Color_Metal_Primary", colorMetalPrimary);
        material.SetColor("_Color_Leather_Secondary", colorLeatherSecondary);
        material.SetColor("_Color_Metal_Secondary", colorMetalSecondary);
        material.SetColor("_Color_Metal_Dark", colorMetalDark);
    }
}
