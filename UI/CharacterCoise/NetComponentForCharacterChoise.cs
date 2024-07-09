using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CharacterData
{
    public CharacterInfo characterInfo;
    public Error error;
}

[System.Serializable]
public class CharacterError
{
    public string errorText;
    public bool isErrored;
}

[System.Serializable]
public class CharacterInfo
{
    public string userMale;
    public string userClass;
    public string nickname;
    public CharacterInfo(string male, string clas, string nick)
    {
        userMale = male;
        userClass = clas;
        nickname = nick;
    }

    public void SetUserMale(string _male) => userMale = _male;
    public void SetUserClass(string _clas) => userClass = _clas;
    public void SetUserName(string _nick) => nickname = _nick;
}
public class NetComponentForCharacterChoise : MonoBehaviour
{
    public CharacterData characterData = new CharacterData();
    [SerializeField] private string targetUrl = "http://localhost/rpg/characters.php";

    public string GetCharacterData(CharacterData data)
    {
        return JsonUtility.ToJson(data);
    }

    public CharacterData SetCharacterData(string data)
    {
        Debug.Log("Raw JSON: " + data); // Added for debugging
        return JsonUtility.FromJson<CharacterData>(data);
    }

    private void Start()
    {
        characterData.error = new Error() { errorText = "text", isErrored = true };
        characterData.characterInfo = new CharacterInfo("man", "healer", "Lisangr");
    }

    public void Registration(string nickname, string userMale, string userClass)
    {
        string userID = PlayerPrefs.GetString("userID", ""); // Get userID from PlayerPrefs
        if (string.IsNullOrEmpty(userID))
        {
            Debug.LogError("User ID is not set in PlayerPrefs.");
            return;
        }
        StopAllCoroutines();
        Registering(userID, nickname, userMale, userClass);
    }

    public void Registering(string userID, string nickname, string userMale, string userClass)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID); // Add userID to the form
        form.AddField("nickname", nickname);
        form.AddField("userMale", userMale);
        form.AddField("userClass", userClass);

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
                Debug.Log("Response: " + responseText); // Debug server response
                characterData = SetCharacterData(responseText);
            }
        }
    }
}
