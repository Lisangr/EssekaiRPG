using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [System.Serializable]
    public class MenuLogin
    {
        public TMP_Text login, password;
    }

    [System.Serializable]
    public class MenuRegistration
    {
        public TMP_Text login, password1, password2, nickname;        
    }

    public MenuLogin loginWindow;
    public MenuRegistration registrationWindow;
    public string userMale, userClass;

    [SerializeField] private NetComponent netComponent;

    public void Login()
    {
        netComponent.Login(loginWindow.login.text, loginWindow.password.text);
    }
    public void Register()
    {
        netComponent.Registration(registrationWindow.login.text, registrationWindow.password1.text,
             registrationWindow.password2.text, registrationWindow.nickname.text,
             userMale, userClass);
    }
}
