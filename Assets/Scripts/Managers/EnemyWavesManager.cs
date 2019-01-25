using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesManager : MonoBehaviour
{
    public static EnemyWavesManager Instance { get; private set; }

    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private LevelParameters levelParameters;
    [SerializeField]
    private EnemyDataSO enemyData;
    [SerializeField]
    private GameObject defaultGun;


    private List<EnemyEntity> currentWaveEntities;
    private List<GameObject> currentWaveEnemyGOs;

    private List<EnemyEntity> toBeKilledEntities;

    private int waveCounter;
    private int maxWaves;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        waveCounter = 0;
        maxWaves = levelParameters.enemiesPerWave.Length;

        toBeKilledEntities = new List<EnemyEntity>();

        int firstWaveCount = levelParameters.enemiesPerWave[waveCounter];
        SpawnWave(waveCounter);
    }

    void Update()
    {
        if (currentWaveEntities.Count == 0 && waveCounter < maxWaves)
        {

        }

        foreach (EnemyEntity enemy in currentWaveEntities)
        {
            enemy.Update();
        }

        KillDeadEnemies();
    }

    public GameObject[] GetCurrentEnemiesGOsArray()
    {
        if (currentWaveEnemyGOs.Count == 0) {
            return System.Array.Empty<GameObject>();
        }

        return currentWaveEnemyGOs.ToArray();
    }

    public void Kill(EnemyEntity enemy)
    {
        toBeKilledEntities.Add(enemy);
    }

    private void SpawnWave(int waveId)
    {
        currentWaveEnemyGOs = new List<GameObject>();

        int enemiesCount = levelParameters.enemiesPerWave[waveId];
        //currentWaveEntities = new EnemyEntity[enemiesCount];
        currentWaveEntities = new List<EnemyEntity>();
        for (int i = 0; i < enemiesCount; i++)
        {
            GameObject enemyGO = SpawnEnemy();
            GameObject currentGun = Instantiate(defaultGun, enemyGO.transform, false);
            currentWaveEnemyGOs.Add(enemyGO);
            currentWaveEntities.Add(new EnemyEntity(0, currentWaveEnemyGOs[i], enemyData, currentGun));
        }
    }

    private GameObject SpawnEnemy()
    {
        Vector3 spawnLocation = UnityEngine.Random.insideUnitSphere * 5;
        spawnLocation.y = 0;

        return (GameObject)Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
        
    }

    private void KillDeadEnemies()
    {
        foreach (EnemyEntity enemy in toBeKilledEntities)
        {
            currentWaveEnemyGOs.Remove(enemy.EnemyGO);
            currentWaveEntities.Remove(enemy);
            Destroy(enemy.EnemyGO);
        }

        toBeKilledEntities = new List<EnemyEntity>();
    }
}
