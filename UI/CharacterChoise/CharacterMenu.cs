using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMenu : MonoBehaviour
{
    [System.Serializable]
    public class CharacterRegistration
    {
        public TMP_Text nickname;
    }

    [SerializeField] private string sceneName = "CharacterCustomization";
    [SerializeField] private NetComponentForCharacterChoise netComponent;
    [SerializeField] private ObjectSwitcher objectSwitcher; // ������ �� ObjectSwitcher

    public CharacterRegistration registrationWindow;

    private string userMale, userClass;
    public void Register()
    {
        string nickname = registrationWindow.nickname.text;
        UpdateCharacterInfo(nickname);
        // ���������� userClass ������ nickname
        netComponent.Registration(nickname, userMale, userClass);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    private void UpdateCharacterInfo(string nickname)
    {
        // �������� ������� �������� ������ �� ObjectSwitcher
        GameObject activeObject = objectSwitcher.objects[objectSwitcher.currentIndex];
        string activeObjectName = activeObject.name;

        int t = objectSwitcher.currentIndex;
        if (t == 1 || t == 3)
        {
            userMale = "Woman";
        }
        else
        {
            userMale = "man";
        }
        // ������������� userClass �� ������ ����� ��������� �������
        userClass = activeObjectName;
    }
}