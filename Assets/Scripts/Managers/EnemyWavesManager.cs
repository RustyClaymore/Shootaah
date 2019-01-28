using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesManager : MonoBehaviour
{
    public static EnemyWavesManager Instance { get; private set; }

    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private LevelParametersSO levelParameters;
    [SerializeField]
    private EnemyDataSO enemyData;
    [SerializeField]
    private GameObject defaultGun;


    private List<EnemyEntity> currentWaveEntities;
    private List<GameObject> currentWaveEnemyGOs;

    private List<EnemyEntity> toBeKilledEntities;

    private int waveCounter;
    private int maxWaves;

    private float cooldownBeforeStartSpawning;
    private bool canSpawnEnemy;

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

        currentWaveEntities = new List<EnemyEntity>();
        currentWaveEnemyGOs = new List<GameObject>();

        cooldownBeforeStartSpawning = levelParameters.timeBeforeWaveSpawn;
        canSpawnEnemy = true;
    }

    void Update()
    {
        if (AllWavesCleared())
        {
            SessionManager.Instance.CompleteMission();
        }

        if (canSpawnEnemy && SessionManager.Instance.IsGameStarted && !SessionManager.Instance.IsGamePaused)
        {
            cooldownBeforeStartSpawning -= Time.deltaTime;
        }

        if (cooldownBeforeStartSpawning < 0)
        {
            canSpawnEnemy = false;
            cooldownBeforeStartSpawning = levelParameters.timeBeforeWaveSpawn;

            SpawnWave(waveCounter);
        }

        foreach (EnemyEntity enemy in currentWaveEntities)
        {
            enemy.Update();
        }

        KillDeadEnemies();
    }

    public GameObject[] GetCurrentEnemiesGOsArray()
    {
        if (currentWaveEnemyGOs.Count == 0)
        {
            return System.Array.Empty<GameObject>();
        }

        return currentWaveEnemyGOs.ToArray();
    }

    public void Kill(EnemyEntity enemy)
    {
        toBeKilledEntities.Add(enemy);
    }

    public bool AllWavesCleared()
    {
        return waveCounter == maxWaves;
    }

    private void SpawnWave(int waveId)
    {
        currentWaveEntities = new List<EnemyEntity>();
        currentWaveEnemyGOs = new List<GameObject>();

        int enemiesCount = levelParameters.enemiesPerWave[waveId];
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
        bool enemiesKilled = false;

        foreach (EnemyEntity enemy in toBeKilledEntities)
        {
            int coinValue = Random.Range(enemy.EnemyData.minCoinValue, enemy.EnemyData.maxCoinValue);
            CollectibleSpawnManager.Instance.SpawnCoinsAtPosition(coinValue, enemy.EnemyGO.transform.position);

            currentWaveEnemyGOs.Remove(enemy.EnemyGO);
            currentWaveEntities.Remove(enemy);
            Destroy(enemy.EnemyGO);
            enemiesKilled = true;
        }

        if (enemiesKilled && currentWaveEntities.Count == 0 && waveCounter < maxWaves)
        {
            waveCounter++;
            canSpawnEnemy = !AllWavesCleared();
        }

        toBeKilledEntities = new List<EnemyEntity>();
    }
}
