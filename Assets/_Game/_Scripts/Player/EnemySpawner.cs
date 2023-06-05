using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Unity.Netcode;
using UnityEngine.UI;
public class EnemySpawner : NetworkBehaviour
{


    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Button ReadyBtn;
    [Header("Attribute")]
    [SerializeField] private int baseEmemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] public int numsOfWave = 5;
    [SerializeField] private int waveRemaining;
    [SerializeField] private TextMeshProUGUI waveLeft;
    [SerializeField] private TextMeshProUGUI announcer;

    [Header("Event")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();


    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyServerRpc);
   
    }

    private void Start()
    {
        if (!IsHost)
        {
            ReadyBtn.enabled = false;
        }
        waveRemaining = numsOfWave;
        waveLeft.text = "Wave Remaining: " + waveRemaining.ToString();
        announcer.text = "Survive for 5 wave";
        Destroy(announcer, 4f);
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            int rand = UnityEngine.Random.Range(0, enemyPrefabs.Length);
            SpawnEnemyClientRpc(rand);
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWaveServerRpc();
            waveRemaining--;
            waveLeft.text = "Wave Remaining: " + waveRemaining.ToString();
            announcer.text = "Wave Cleared";    
            Destroy(announcer, 4f);
        }
    }


[ServerRpc]
    private void EndWaveServerRpc()
    {
        EndWaveClientRpc();
    }
[ClientRpc]
    private void EndWaveClientRpc()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }
[ServerRpc]
    private void EnemyDestroyServerRpc()
    {
        EnemyDestroyClientRpc();
    }
[ClientRpc]
    private void EnemyDestroyClientRpc()
    {
        enemiesAlive--;
    }


    private IEnumerator StartWave()
    {
        if(waveRemaining > 0)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            isSpawning = true;
            enemiesLeftToSpawn = EnemiesPerWave();
        }
        
    }
[ClientRpc]
    private void SpawnEnemyClientRpc(int rand )
    {
        GameObject prefabToSpawn = enemyPrefabs[rand];
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        prefabToSpawn.GetComponent<NetworkObject>();
    }


    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEmemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    public int getWaveRemaining()
    {
        return waveRemaining;
    }

}
