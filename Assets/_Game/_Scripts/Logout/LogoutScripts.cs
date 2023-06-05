using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LogoutScripts : MonoBehaviour
{
    string baseUrl = "http://localhost/Hunting_Rifle_database/";
    //public TMP_InputField accountUserName;
    //public TMP_InputField accountPassword;
    //public TMP_Text info;
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
                //info.text = "You are not loged in.";
            }
        }
        else
        {
            //info.text = "You are not loged in.";
        }
    }
    public void AccountLogout()
    {
        currentUsername = "";
        PlayerPrefs.SetString(ukey, currentUsername);
        //info.text = "You are just logged out!";
        SceneManager.LoadScene("LoginScene");
    }
    
}
