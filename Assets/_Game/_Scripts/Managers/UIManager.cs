using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
public class UIManager : NetworkBehaviour
{
    [SerializeField] private GameObject Winpanel;
    [SerializeField] private GameObject Losepanel;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private PlayerCount count;
    // Start is called before the first frame update
    void Start()
    {
        Winpanel.SetActive(false);
        Losepanel.SetActive(false);
    }
    private void Update()
    {
        Check();
    }

  
    private void Check()
    {
        if (!IsHost) return;
        if (spawner.getWaveRemaining() == 0 && count.getNumsOfPlayer() != 0)
        {
            ShowWinpanelClientRpc();
        }
        if (count.getNumsOfPlayer() == 0)
        {
            ShowLosepanelClientRpc();
        }
    }
[ClientRpc]
    private void ShowWinpanelClientRpc()
    {
        Winpanel.SetActive(true);
    }
[ClientRpc]
    private void ShowLosepanelClientRpc()
    {
        Losepanel.SetActive(true);
    }
    public void NextbuttonCallback()
    {
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-2);
    }
}
