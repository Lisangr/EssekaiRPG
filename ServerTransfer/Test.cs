using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class UserData
{
    public PlayerInfo playerInfo;
    public Error error;
}

[System.Serializable]
public class Error
{
    public string errorText;
    public bool isErrored;
}

[System.Serializable]
public class PlayerInfo
{
    public string userMale;
    public string userClass;
    public string nickname;
    public PlayerInfo(string male, string clas, string nick)
    {
        userMale = male;
        userClass = clas;
        nickname = nick;
    }

    public void SetUserMale(string _male) => userMale = _male;
    public void SetUserClass(string _clas) => userClass = _clas;
    public void SetUserName(string _nick) => nickname = _nick;
}

public class Test : MonoBehaviour
{
    public UserData userData = new UserData();
    [SerializeField] private string targetUrl = "http://localhost/rpg/logreg.php";
    //[SerializeField] UserData returnedData;
    public string GetUserData(UserData data)
    {
        return JsonUtility.ToJson(data);
    }
    public UserData SetUserData(string data)
    {
        Debug.Log("Raw JSON: " + data); // Добавлено для отладки
        return JsonUtility.FromJson<UserData>(data);
    }

    private void Start()
    {
        userData.error = new Error() { errorText = "text", isErrored = true };
        userData.playerInfo = new PlayerInfo("man", "healer", "Lisangr");
    }

    public void Login(string login, string password)
    {
        StopAllCoroutines();
        Logining(login, password);
    }

    public void Logining(string login, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", "logining");
        form.AddField("login", login);
        form.AddField("password", password);

        StartCoroutine(SendData(form));
    }
    public void Registration(string login, string password1, string password2, string nickname, string userMale, string userClass)
    {
        StopAllCoroutines();
        Registering(login, password1, password2, nickname, userMale, userClass);
    }

    public void Registering(string login, string password1, string password2, string nickname, string userMale, string userClass)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", "register");
        form.AddField("login", login);
        form.AddField("password1", password1);
        form.AddField("password2", password2);
        form.AddField("nickname", nickname); // Добавлено поле nickname
        form.AddField("userMale", userMale); // Добавлено поле userMale
        form.AddField("userClass", userClass); // Добавлено поле userClass

        StartCoroutine(SendData(form));
    }
    private IEnumerator SendData(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(targetUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Response: " + responseText); // Отладка ответа сервера
                userData = SetUserData(responseText);
            }
        }
    }
}