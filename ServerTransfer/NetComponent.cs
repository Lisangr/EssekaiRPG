using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[System.Serializable]
public class UserData
{
    public PlayerInfo playerInfo;
    public Error error;
    public string userID;
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
    public bool hasCharacters;
    public string nickname;
    public string userMale;
    public string userClass;
}

public class NetComponent : MonoBehaviour
{
    public UserData userData = new UserData();
    public string userID;
    [SerializeField] private string targetUrl = "http://localhost/rpg/logreg.php";
    [SerializeField] private string sceneName;
    [SerializeField] private string lobbySceneName;
    [SerializeField] private GameObject errorForm;

    public string GetUserData(UserData data)
    {
        return JsonUtility.ToJson(data);
    }

    public UserData SetUserData(string data)
    {
        Debug.Log("Raw JSON: " + data);
        return JsonUtility.FromJson<UserData>(data);
    }

    private void Start()
    {
        userData.error = new Error() { errorText = "text", isErrored = true };
        userData.playerInfo = new PlayerInfo();
    }

    public void Login(string login, string password)
    {
        StopAllCoroutines();
        Logining(login, password);
    }

    public void Registration(string login, string password1, string password2)
    {
        StopAllCoroutines();
        Registering(login, password1, password2);
    }

    public void Registering(string login, string password1, string password2)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", "register");
        form.AddField("login", login);
        form.AddField("password1", password1);
        form.AddField("password2", password2);

        StartCoroutine(SendData(form));
    }

    public void Logining(string login, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", "logining");
        form.AddField("login", login);
        form.AddField("password", password);

        StartCoroutine(SendData(form, OnLoginResponse));
    }

    private IEnumerator SendData(WWWForm form, Action<string> callback)
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
                Debug.Log("Response: " + responseText);
                callback?.Invoke(responseText);
            }
        }
    }

    private void OnLoginResponse(string jsonResponse)
    {
        UserData response = SetUserData(jsonResponse);

        if (response.error.isErrored)
        {
            errorForm.SetActive(true);
            Debug.LogError("Login failed: " + response.error.errorText);
        }
        else
        {
            Debug.Log("Login successful.");
            userID = response.userID;
            PlayerPrefs.SetString("userID", userID);
            PlayerPrefs.Save();

            Debug.Log("Player has characters: " + response.playerInfo.hasCharacters);
            Debug.Log("Player nickname: " + response.playerInfo.nickname);
            Debug.Log("Player userMale: " + response.playerInfo.userMale);
            Debug.Log("Player userClass: " + response.playerInfo.userClass);

            if (response.playerInfo.hasCharacters &&
                !string.IsNullOrEmpty(response.playerInfo.nickname) &&
                !string.IsNullOrEmpty(response.playerInfo.userMale) &&
                !string.IsNullOrEmpty(response.playerInfo.userClass))
            {
                Debug.Log("Loading lobby scene...");
                SceneManager.LoadScene(lobbySceneName, LoadSceneMode.Single);
            }
            else
            {
                Debug.Log("Loading main scene...");
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
        }
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
                Debug.Log("Response: " + responseText);
                userData = SetUserData(responseText);
            }
        }
    }
}
