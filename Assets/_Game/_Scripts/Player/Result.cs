using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI announcer;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private PlayerCounter counter;

    // Update is called once per frame
    void Update()
    {
        Victory();
        Defeat();
    }

    private void Victory()
    {
        if(spawner.getWaveRemaining() == 0 && counter.getNumsOfPlayer() != 0) 
        {
            announcer.color = Color.yellow;
            announcer.text = "VICTORY";    
        }
    }

    private void Defeat()
    {
        if(counter.getNumsOfPlayer() == 0)
        {
            announcer.color = Color.red;
            announcer.text = "DEFEAT";
        }
    }


}
