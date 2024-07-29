using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
public class CharacterLoader : MonoBehaviour
{
    public TextMeshProUGUI nickname;
    public TextMeshProUGUI characterLvl;
    public TextMeshProUGUI characterExp;
    public Transform spawnPoint;

    public GameObject nextButton;
    public GameObject prevButton;
    public GameObject maleElfPrefab;
    public GameObject femaleElfPrefab;
    public GameObject maleHumanPrefab;
    public GameObject femaleHumanPrefab;

    private string url = "http://localhost/rpg/loader.php"; // Замените на ваш актуальный URL
    private List<Character> characters;
    private int currentIndex = 0;

    private GameObject currentCharacterInstance;
    void Start()
    {
        nextButton.SetActive(false);
        prevButton.SetActive(false);
        StartCoroutine(GetCharacterData());
    }

    IEnumerator GetCharacterData()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.certificateHandler = new BypassCertificate(); // Игнорировать ошибки сертификата
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Response: " + www.downloadHandler.text); // Вывод JSON-ответа в консоль
            CharacterList characterList = JsonUtility.FromJson<CharacterList>(www.downloadHandler.text);

            characters = characterList.characters.FindAll(character =>
                !string.IsNullOrEmpty(character.nickname));

            if (characters.Count > 0)
            {
                DisplayCharacter(currentIndex);
                if (characters.Count > 1)
                {
                    nextButton.SetActive(true);
                }
            }
        }
    }

    void DisplayCharacter(int index)
    {
        if (index >= 0 && index < characters.Count)
        {
            Character character = characters[index];
            nickname.text = character.nickname;
            characterLvl.text = "lvl: " + character.lvl.ToString();
            characterExp.text = "exp: " + character.exp.ToString();

            // Отключаем предыдущий экземпляр персонажа
            if (currentCharacterInstance != null)
            {
                Destroy(currentCharacterInstance);
            }

            // Определяем префаб на основе пола и класса
            GameObject characterPrefab = null;
            if (character.userMale && character.userClass == "Elf")
            {
                characterPrefab = maleElfPrefab;
            }
            else if (!character.userMale && character.userClass == "Elf")
            {
                characterPrefab = femaleElfPrefab;
            }
            else if (character.userMale && character.userClass == "Human")
            {
                characterPrefab = maleHumanPrefab;
            }
            else if (!character.userMale && character.userClass == "Human")
            {
                characterPrefab = femaleHumanPrefab;
            }

            if (characterPrefab != null)
            {
                // Создаем новый экземпляр персонажа
                currentCharacterInstance = Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);
                currentCharacterInstance.SetActive(true);

                ApplyCustomisation(currentCharacterInstance, character.customiser);
            }
        }

        prevButton.SetActive(index > 0);
        nextButton.SetActive(index < characters.Count - 1);
    }

    void ApplyCustomisation(GameObject characterInstance, Customiser customiser)
    {
        characterInstance.transform.localScale = new Vector3(customiser.scaleX, customiser.scaleY, customiser.scaleZ);

        Renderer[] renderers = characterInstance.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] newMaterials = new Material[renderer.materials.Length];
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                newMaterials[i] = new Material(renderer.materials[i]);
            }
            renderer.materials = newMaterials;
            foreach (Material material in newMaterials)
            {
                UpdateColors(material, customiser);
            }
        }
    }

    void UpdateColors(Material material, Customiser customiser)
    {
        Color colorHair = new Color(customiser.HairSliderR, customiser.HairSliderG, customiser.HairSliderB);
        Color colorSkin = new Color(customiser.SkinSliderR, customiser.SkinSliderG, customiser.SkinSliderB);
        Color colorBodyArt = new Color(customiser.BodyArtSliderR, customiser.BodyArtSliderG, customiser.BodyArtSliderB);
        Color colorEyes = new Color(customiser.EyesSliderR, customiser.EyesSliderG, customiser.EyesSliderB);
        Color colorPrimary = new Color(customiser.PrimarySliderR, customiser.PrimarySliderG, customiser.PrimarySliderB);
        Color colorSecondary = new Color(customiser.SecondarySliderR, customiser.SecondarySliderG, customiser.SecondarySliderB);
        Color colorLeatherPrimary = new Color(customiser.LeatherPrimarySliderR, customiser.LeatherPrimarySliderG, customiser.LeatherPrimarySliderB);
        Color colorMetalPrimary = new Color(customiser.MetalPrimarySliderR, customiser.MetalPrimarySliderG, customiser.MetalPrimarySliderB);
        Color colorLeatherSecondary = new Color(customiser.LeatherSecondarySliderR, customiser.LeatherSecondarySliderG, customiser.LeatherSecondarySliderB);
        Color colorMetalSecondary = new Color(customiser.MetalSecondarySliderR, customiser.MetalSecondarySliderG, customiser.MetalSecondarySliderB);
        Color colorMetalDark = new Color(customiser.MetalDarkSliderR, customiser.MetalDarkSliderG, customiser.MetalDarkSliderB);

        ApplyColors(material, colorHair, colorSkin, colorBodyArt, colorEyes, colorPrimary, colorSecondary, colorLeatherPrimary, colorMetalPrimary, colorLeatherSecondary, colorMetalSecondary, colorMetalDark);
    }

    void ApplyColors(Material material, Color colorHair, Color colorSkin, Color colorBodyArt, Color colorEyes, Color colorPrimary, Color colorSecondary, Color colorLeatherPrimary, Color colorMetalPrimary, Color colorLeatherSecondary, Color colorMetalSecondary, Color colorMetalDark)
    {
        material.SetColor("_Color_Hair", colorHair);
        material.SetColor("_Color_Skin", colorSkin);
        material.SetColor("_Color_Scar", colorBodyArt);
        material.SetColor("_Color_Eyes", colorEyes);
        material.SetColor("_Color_Primary", colorPrimary);
        material.SetColor("_Color_Secondary", colorSecondary);
        material.SetColor("_Color_Leather_Primary", colorLeatherPrimary);
        material.SetColor("_Color_Metal_Primary", colorMetalPrimary);
        material.SetColor("_Color_Leather_Secondary", colorLeatherSecondary);
        material.SetColor("_Color_Metal_Secondary", colorMetalSecondary);
        material.SetColor("_Color_Metal_Dark", colorMetalDark);
    }

    public void NextCharacter()
    {
        if (currentIndex < characters.Count - 1)
        {
            currentIndex++;
            DisplayCharacter(currentIndex);
        }
    }

    public void PrevCharacter()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            DisplayCharacter(currentIndex);
        }
    }

    public Customiser GetCurrentCustomiser()
    {
        if (currentIndex >= 0 && currentIndex < characters.Count)
        {
            return characters[currentIndex].customiser;
        }
        return null;
    }

    public void SaveCustomisation(Customiser customiser)
    {
        CustomisationData data = new CustomisationData
        {
            scaleX = customiser.scaleX / 30f,
            scaleY = customiser.scaleY / 30f,
            scaleZ = customiser.scaleZ / 30f,
            HairSliderR = customiser.HairSliderR,
            HairSliderG = customiser.HairSliderG,
            HairSliderB = customiser.HairSliderB,
            SkinSliderR = customiser.SkinSliderR,
            SkinSliderG = customiser.SkinSliderG,
            SkinSliderB = customiser.SkinSliderB,
            BodyArtSliderR = customiser.BodyArtSliderR,
            BodyArtSliderG = customiser.BodyArtSliderG,
            BodyArtSliderB = customiser.BodyArtSliderB,
            EyesSliderR = customiser.EyesSliderR,
            EyesSliderG = customiser.EyesSliderG,
            EyesSliderB = customiser.EyesSliderB,
            PrimarySliderR = customiser.PrimarySliderR,
            PrimarySliderG = customiser.PrimarySliderG,
            PrimarySliderB = customiser.PrimarySliderB,
            SecondarySliderR = customiser.SecondarySliderR,
            SecondarySliderG = customiser.SecondarySliderG,
            SecondarySliderB = customiser.SecondarySliderB,
            LeatherPrimarySliderR = customiser.LeatherPrimarySliderR,
            LeatherPrimarySliderG = customiser.LeatherPrimarySliderG,
            LeatherPrimarySliderB = customiser.LeatherPrimarySliderB,
            MetalPrimarySliderR = customiser.MetalPrimarySliderR,
            MetalPrimarySliderG = customiser.MetalPrimarySliderG,
            MetalPrimarySliderB = customiser.MetalPrimarySliderB,
            LeatherSecondarySliderR = customiser.LeatherSecondarySliderR,
            LeatherSecondarySliderG = customiser.LeatherSecondarySliderG,
            LeatherSecondarySliderB = customiser.LeatherSecondarySliderB,
            MetalSecondarySliderR = customiser.MetalSecondarySliderR,
            MetalSecondarySliderG = customiser.MetalSecondarySliderG,
            MetalSecondarySliderB = customiser.MetalSecondarySliderB,
            MetalDarkSliderR = customiser.MetalDarkSliderR,
            MetalDarkSliderG = customiser.MetalDarkSliderG,
            MetalDarkSliderB = customiser.MetalDarkSliderB
        };

        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("CustomisationData", jsonData);
    }
    
    public void LoadCustomisation(GameObject characterInstance)
    {
        if (PlayerPrefs.HasKey("CustomisationData"))
        {
            string jsonData = PlayerPrefs.GetString("CustomisationData");
            CustomisationData data = JsonUtility.FromJson<CustomisationData>(jsonData);

            Customiser customiser = new Customiser
            {
                scaleX = data.scaleX,
                scaleY = data.scaleY,
                scaleZ = data.scaleZ,
                HairSliderR = data.HairSliderR,
                HairSliderG = data.HairSliderG,
                HairSliderB = data.HairSliderB,
                SkinSliderR = data.SkinSliderR,
                SkinSliderG = data.SkinSliderG,
                SkinSliderB = data.SkinSliderB,
                BodyArtSliderR = data.BodyArtSliderR,
                BodyArtSliderG = data.BodyArtSliderG,
                BodyArtSliderB = data.BodyArtSliderB,
                EyesSliderR = data.EyesSliderR,
                EyesSliderG = data.EyesSliderG,
                EyesSliderB = data.EyesSliderB,
                PrimarySliderR = data.PrimarySliderR,
                PrimarySliderG = data.PrimarySliderG,
                PrimarySliderB = data.PrimarySliderB,
                SecondarySliderR = data.SecondarySliderR,
                SecondarySliderG = data.SecondarySliderG,
                SecondarySliderB = data.SecondarySliderB,
                LeatherPrimarySliderR = data.LeatherPrimarySliderR,
                LeatherPrimarySliderG = data.LeatherPrimarySliderG,
                LeatherPrimarySliderB = data.LeatherPrimarySliderB,
                MetalPrimarySliderR = data.MetalPrimarySliderR,
                MetalPrimarySliderG = data.MetalPrimarySliderG,
                MetalPrimarySliderB = data.MetalPrimarySliderB,
                LeatherSecondarySliderR = data.LeatherSecondarySliderR,
                LeatherSecondarySliderG = data.LeatherSecondarySliderG,
                LeatherSecondarySliderB = data.LeatherSecondarySliderB,
                MetalSecondarySliderR = data.MetalSecondarySliderR,
                MetalSecondarySliderG = data.MetalSecondarySliderG,
                MetalSecondarySliderB = data.MetalSecondarySliderB,
                MetalDarkSliderR = data.MetalDarkSliderR,
                MetalDarkSliderG = data.MetalDarkSliderG,
                MetalDarkSliderB = data.MetalDarkSliderB
            };

            ApplyCustomisation(characterInstance, customiser);
        }
    }
}

public class BypassCertificate : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true; // Игнорировать проверку сертификата
    }
}

[System.Serializable]
public class Character
{
    public int id;
    public string nickname;
    public int lvl;
    public int exp;
    public bool userMale;
    public string userClass;
    public Customiser customiser;
}

[System.Serializable]
public class Customiser
{
    public float scaleX;
    public float scaleY;
    public float scaleZ;
    public float HairSliderR;
    public float HairSliderG;
    public float HairSliderB;
    public float SkinSliderR;
    public float SkinSliderG;
    public float SkinSliderB;
    public float BodyArtSliderR;
    public float BodyArtSliderG;
    public float BodyArtSliderB;
    public float EyesSliderR;
    public float EyesSliderG;
    public float EyesSliderB;
    public float PrimarySliderR;
    public float PrimarySliderG;
    public float PrimarySliderB;
    public float SecondarySliderR;
    public float SecondarySliderG;
    public float SecondarySliderB;
    public float LeatherPrimarySliderR;
    public float LeatherPrimarySliderG;
    public float LeatherPrimarySliderB;
    public float MetalPrimarySliderR;
    public float MetalPrimarySliderG;
    public float MetalPrimarySliderB;
    public float LeatherSecondarySliderR;
    public float LeatherSecondarySliderG;
    public float LeatherSecondarySliderB;
    public float MetalSecondarySliderR;
    public float MetalSecondarySliderG;
    public float MetalSecondarySliderB;
    public float MetalDarkSliderR;
    public float MetalDarkSliderG;
    public float MetalDarkSliderB;
}

[System.Serializable]
public class CharacterList
{
    public List<Character> characters;
}

[System.Serializable]
public class CustomisationData
{
    public float scaleX, scaleY, scaleZ;
    public float HairSliderR, HairSliderG, HairSliderB;
    public float SkinSliderR, SkinSliderG, SkinSliderB;
    public float BodyArtSliderR, BodyArtSliderG, BodyArtSliderB;
    public float EyesSliderR, EyesSliderG, EyesSliderB;
    public float PrimarySliderR, PrimarySliderG, PrimarySliderB;
    public float SecondarySliderR, SecondarySliderG, SecondarySliderB;
    public float LeatherPrimarySliderR, LeatherPrimarySliderG, LeatherPrimarySliderB;
    public float MetalPrimarySliderR, MetalPrimarySliderG, MetalPrimarySliderB;
    public float LeatherSecondarySliderR, LeatherSecondarySliderG, LeatherSecondarySliderB;
    public float MetalSecondarySliderR, MetalSecondarySliderG, MetalSecondarySliderB;
    public float MetalDarkSliderR, MetalDarkSliderG, MetalDarkSliderB;
}