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

    [SerializeField] private NetComponentForCharacterChoise netComponent;
    [SerializeField] private ObjectSwitcher objectSwitcher; // ������ �� ObjectSwitcher

    public void Register()
    {
        string nickname = registrationWindow.nickname.text;
        UpdateCharacterInfo(nickname);
        // ���������� userClass ������ nickname
        netComponent.Registration(nickname, userMale, userClass);
    }

    private void UpdateCharacterInfo(string nickname)
    {
        // �������� ������� �������� ������ �� ObjectSwitcher
        GameObject activeObject = objectSwitcher.objects[objectSwitcher.currentIndex];
        string activeObjectName = activeObject.name;

        switch (activeObjectName)
        {
            case "Archer":
            case "Healler":
                userMale = "woman";
                break;
            default:
                userMale = "man";
                break;
        }

        // ������������� userClass �� ������ ����� ��������� �������
        userClass = activeObjectName;
    }
}
