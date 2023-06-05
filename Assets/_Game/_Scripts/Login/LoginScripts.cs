using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScripts : MonoBehaviour
{
    string baseUrl = "http://localhost/Hunting_Rifle_database/";
    public TMP_InputField accountUserName;
    public TMP_InputField accountPassword;
    public TMP_Text info;
    private string currentUsername;
    private string ukey = "accountusername";
    // Start is called before the first frame update
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

    public void toRegister()
    {
        SceneManager.LoadScene("Register");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void AccountLogin()
    {
        string uName = accountUserName.text;
        string pWord = accountPassword.text;
        StartCoroutine(LogInAccount(uName, pWord));
    }
    IEnumerator LogInAccount(string uName, string pWord)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUsername", uName);
        form.AddField("loginPassword", pWord);
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
                if (responseText == "1")
                {
                    PlayerPrefs.SetString(ukey, uName);
                    //info.text = "Login success with username " + uName;
                    SceneManager.LoadSceneAsync("Auth");

                }
                else
                {
                    info.text = "Login failed!";
                }
            }
        }
    }
}
