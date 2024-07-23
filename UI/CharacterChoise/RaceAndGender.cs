using UnityEngine;

public enum Races { Elf, Human }
public enum Genders { Male, Female }
public class RaceAndGender : MonoBehaviour
{
    public Races selectedRace;
    public Genders selectedGender;

    public GameObject elfMale;
    public GameObject elfFemale;
    public GameObject humanMale;
    public GameObject humanFemale;

    public static bool isFemale;
    private void Awake()
    {
        // Cache and deactivate all characters first
        CacheAndDeactivateCharacters();

        // Set default selections
        selectedRace = Races.Human;
        selectedGender = Genders.Male;

        // Activate the default character
        UpdateActiveCharacter();

        isFemale = false;
    }
    private void CacheAndDeactivateCharacters()
    {
        elfMale.SetActive(false);
        elfFemale.SetActive(false);
        humanMale.SetActive(false);
        humanFemale.SetActive(false);
    }
    public void SelectElf()
    {
        selectedRace = Races.Elf;
        Debug.Log("Race selected: Elf");
        UpdateActiveCharacter();
    }

    public void SelectHuman()
    {
        selectedRace = Races.Human;
        Debug.Log("Race selected: Human");
        UpdateActiveCharacter();
    }

    public void SelectMale()
    {
        selectedGender = Genders.Male;
        isFemale = false;
        Debug.Log("Gender selected: Male");
        UpdateActiveCharacter();
    }

    public void SelectFemale()
    {
        selectedGender = Genders.Female;
        isFemale = true;
        Debug.Log("Gender selected: Female");
        UpdateActiveCharacter();
    }

    private void UpdateActiveCharacter()
    {
        // Deactivate all characters first
        CacheAndDeactivateCharacters();

        // Activate the selected character
        if (selectedRace == Races.Elf && selectedGender == Genders.Male)
        {
            elfMale.SetActive(true);
        }
        else if (selectedRace == Races.Elf && selectedGender == Genders.Female)
        {
            elfFemale.SetActive(true);
        }
        else if (selectedRace == Races.Human && selectedGender == Genders.Male)
        {
            humanMale.SetActive(true);
        }
        else if (selectedRace == Races.Human && selectedGender == Genders.Female)
        {
            humanFemale.SetActive(true);
        }
    }
}
