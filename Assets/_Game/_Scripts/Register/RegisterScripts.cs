using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RegisterScripts : MonoBehaviour
{
    // Start is called before the first frame update
    string baseUrl = "http://localhost/Hunting_Rifle_database/";
    public TMP_InputField accountUserName;
    public TMP_InputField accountPassword;
    public TMP_Text info;
    private string currentUsername;
    private string ukey = "accountusername";
    void Start()
    {
        currentUsername = "";

        if (PlayerPrefs.HasKey(ukey))
        {
            if (PlayerPrefs.GetString(ukey) != "")
            {
                currentUsername = PlayerPrefs.GetString(ukey);
                //info.text = "You are loged in = " + currentUsername;
            }
            else
            {
                info.text = "You are not loged in.";
            }
        }
        else
        {
            info.text = "You are not loged in.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToLogin()
    {
        SceneManager.LoadScene("LoginScene");
    }
    public void AccountRegister()
    {
        string uName = accountUserName.text;
        string pWord = accountPassword.text;
        StartCoroutine(RegisterNewAccount(uName, pWord));
    }
    IEnumerator RegisterNewAccount(string uName, string pWord)
    {
        WWWForm form = new WWWForm();
        form.AddField("newAccountUsername", uName);
        form.AddField("newAccountPassword", pWord);
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Response = " + responseText);
                info.text = "Response = " + responseText;
            }
        }
    }
}
