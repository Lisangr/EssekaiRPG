using TMPro;
using UnityEngine;

public class CharacterMenu : MonoBehaviour
{
    [System.Serializable]
    public class CharacterRegistration
    {
        public TMP_Text nickname;
    }

    public CharacterRegistration registrationWindow;
    public string userMale, userClass;
    public string userID; // Add a field for userID

    [SerializeField] private NetComponentForCharacterChoise netComponent;
    public void SetUserID(string id)
    {
        userID = id;
    }
    public void Register()
    {
        string nickname = registrationWindow.nickname.text;
        UpdateCharacterInfo(nickname);
        netComponent.Registration(nickname, userMale, userClass); // Pass userID to Registration method
    }

    private void UpdateCharacterInfo(string nickname)
    {
        switch (nickname)
        {
            case "Archer":
            case "Healer":
                userMale = "woman";
                break;
            default:
                userMale = "man";
                break;
        }

        userClass = nickname; // Assuming the nickname corresponds to the class
    }
    /*
    [System.Serializable]
    public class CharacterRegistration
    {
        public TMP_Text nickname;
    }

    public CharacterRegistration registrationWindow;
    public string userMale, userClass;

    [SerializeField] private NetComponentForCharacterChoise netComponent;

    public void Register()
    {
        netComponent.Registration(registrationWindow.nickname.text, userMale, userClass);
    }*/
}
