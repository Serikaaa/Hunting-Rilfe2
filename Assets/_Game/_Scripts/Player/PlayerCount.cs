using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerCount : NetworkBehaviour
{
    // Start is called before the first frame update
    private int playerCount = 0;
    private NetworkClient a ;
    void Start()
    {
        if (!IsHost) return;
        NetworkManager networkManager = NetworkManager.Singleton;
        if (networkManager != null)
        {

            foreach (NetworkClient client in networkManager.ConnectedClientsList)
            {
                if (client.PlayerObject != null)
                {
                    playerCount++;
                    Debug.Log("playerCountStart: " + playerCount);
                }
            }
        }
    }
    private void Update()
    {
        if (!IsHost) return;

        NetworkManager networkManager = NetworkManager.Singleton;

        if (networkManager != null)
        {
            foreach (NetworkClient client in networkManager.ConnectedClientsList)
            {
                if (client.PlayerObject == null && client!=a)
                {
                    a = client;
                    playerCount--;
                    Debug.Log("playerCountUpdate: " + playerCount);
                }
            }
        }
    }
    public int getNumsOfPlayer()
    {
        return playerCount;
    }
}
