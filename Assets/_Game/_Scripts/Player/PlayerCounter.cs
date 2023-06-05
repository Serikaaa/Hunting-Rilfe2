using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerCounter : MonoBehaviour
{
    // Start is called before the first frame update

    private int playerCount = 0;
    void Start()
    {
        NetworkManager networkManager = NetworkManager.Singleton;
        if (networkManager != null)
        {

            foreach (NetworkClient client in networkManager.ConnectedClientsList)
            {
                if (client.PlayerObject != null)
                {
                    playerCount++;
                }
            }
        }
    }

    public int getNumsOfPlayer()
    {
        return playerCount;
    }
}
