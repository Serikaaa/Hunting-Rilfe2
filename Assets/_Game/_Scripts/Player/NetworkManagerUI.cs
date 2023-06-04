using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button hostbtn;
    [SerializeField] private Button clientbtn;

    private void Awake()
    {
        hostbtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
        clientbtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
