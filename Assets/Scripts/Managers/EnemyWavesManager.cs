using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyWavesManager : MonoBehaviour
{
    public static EnemyWavesManager Instance { get; private set; }

    [SerializeField]
    private LevelParametersSO levelParameters;
    [SerializeField]
    private Transform[] spawnLocations;
    [SerializeField]
    private GameObject[] enemyPrefabs;
    [SerializeField]
    private EnemyDataSO[] enemyData;
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

            StartCoroutine(SpawnWave(waveCounter));
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

    private IEnumerator SpawnWave(int waveId)
    {
        currentWaveEntities = new List<EnemyEntity>();
        currentWaveEnemyGOs = new List<GameObject>();

        int enemiesCount = levelParameters.enemiesPerWave[waveId];
        int[] spawnedEnemyTypes = new int[enemiesCount];
        Vector3[] spawnedEnemyLocations = new Vector3[enemiesCount];

        SpawnEnemySpawnIndictarors(enemiesCount, spawnedEnemyTypes, spawnedEnemyLocations);
        yield return new WaitForSeconds(1.5f);
        SpawnEnemies(enemiesCount, spawnedEnemyTypes, spawnedEnemyLocations);
    }

    private void SpawnEnemySpawnIndictarors(int enemiesCount, int[] spawnedEnemyTypes, Vector3[] spawnedEnemyLocations)
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            int randEnemyType = Random.Range(0, 2);
            spawnedEnemyTypes[i] = randEnemyType;

            int randomSpawnLocation = UnityEngine.Random.Range(0, spawnLocations.Length);
            Vector3 spawnLocation = spawnLocations[randomSpawnLocation].position + UnityEngine.Random.insideUnitSphere * 5;
            spawnLocation.y = 0;
            spawnedEnemyLocations[i] = spawnLocation;

            ParticlesManager.Instance.InstantiateEnemySpawnParticle(spawnLocation, 1.4f);
        }
    }

    private void SpawnEnemies(int enemiesCount, int[] spawnedEnemyTypes, Vector3[] spawnedEnemyLocations)
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            GameObject enemyGO = SpawnEnemy(spawnedEnemyTypes[i], spawnedEnemyLocations[i]);

            GameObject currentGun = null;
            if ((spawnedEnemyTypes[i] + 1) == (int)EntityType.enemyFighterType)
            {
                currentGun = Instantiate(defaultGun, enemyGO.transform, false);
            }

            currentWaveEnemyGOs.Add(enemyGO);
            EnemyEntity enemy = new EnemyEntity(
                0,
                spawnedEnemyTypes[i] + 1,
                currentWaveEnemyGOs[i],
                enemyData[spawnedEnemyTypes[i]],
                currentGun
            );
            currentWaveEntities.Add(enemy);
        }
    }

    private GameObject SpawnEnemy(int enemyType, Vector3 position)
    {
        return (GameObject)Instantiate(enemyPrefabs[enemyType], position, Quaternion.identity);

    }

    private void KillDeadEnemies()
    {
        bool enemiesKilled = false;

        foreach (EnemyEntity enemy in toBeKilledEntities)
        {
            int diamondValue = Random.Range(enemy.EnemyData.minDiamondValue, enemy.EnemyData.maxDiamondValue);
            CollectibleSpawnManager.Instance.SpawnDiamondsAtPosition(diamondValue, enemy.EnemyGO.transform.position);

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
