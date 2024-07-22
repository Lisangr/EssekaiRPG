using System;
using System.Collections;
using System.Linq;
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
    public PlayerInfo()
    {
    }
}

public class NetComponent : MonoBehaviour
{
    public UserData userData = new UserData();
    public string userID; // Add field to store userID
    [SerializeField] private string targetUrl = "http://localhost/rpg/logreg.php";
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject errorForm;

    public string GetUserData(UserData data)
    {
        return JsonUtility.ToJson(data);
    }

    public UserData SetUserData(string data)
    {
        Debug.Log("Raw JSON: " + data); // Added for debugging
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
                Debug.Log("Response: " + responseText); // Debug server response
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
            // Handle error (e.g., display message to user)
        }
        else
        {
            Debug.Log("Login successful.");
            userID = response.userID; // Store the userID
            PlayerPrefs.SetString("userID", userID); // Save userID to PlayerPrefs
            PlayerPrefs.Save(); // Ensure the data is written to disk
                                // Load the scene after successful login
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
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
                Debug.Log("Response: " + responseText); // Debug server response
                userData = SetUserData(responseText);
            }
        }
    }
}