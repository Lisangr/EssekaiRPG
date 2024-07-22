using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMenu : MonoBehaviour
{
    [System.Serializable]
    public class CharacterRegistration
    {
        public TMP_Text nickname;
    }
    
    [SerializeField] private string sceneName = "Summon 1";
    [SerializeField] private NetComponentForCharacterChoise netComponent;
    [SerializeField] private ObjectSwitcher objectSwitcher; // Ссылка на ObjectSwitcher

    public CharacterRegistration registrationWindow;

    private string userMale, userClass;
    public void Register()
    {
        string nickname = registrationWindow.nickname.text;
        UpdateCharacterInfo(nickname);
        // Используем userClass вместо nickname
        netComponent.Registration(nickname, userMale, userClass);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    private void UpdateCharacterInfo(string nickname)
    {
        // Получаем текущий активный объект из ObjectSwitcher
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

        // Устанавливаем userClass на основе имени активного объекта
        userClass = activeObjectName;
    }
}
