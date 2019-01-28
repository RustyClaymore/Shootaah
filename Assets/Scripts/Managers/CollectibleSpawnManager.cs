using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawnManager : MonoBehaviour
{
    public static CollectibleSpawnManager Instance { get; private set; }

    public GameObject smallCoinPrefab;
    public GameObject mediumCoinPrefab;
    public GameObject largeCoinPrefab;

    private Dictionary<string, GameObject> coinPrefabs;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        coinPrefabs = new Dictionary<string, GameObject>();
        coinPrefabs[Coin.SmallCoin] = smallCoinPrefab;
        coinPrefabs[Coin.MediumCoin] = mediumCoinPrefab;
        coinPrefabs[Coin.LargeCoin] = largeCoinPrefab;
    }

    public void SpawnCoinsAtPosition(int coinAmount, Vector3 position)
    {
        int numLargeCoin = coinAmount / 10;
        int numMediumCoin = (coinAmount - 10 * numLargeCoin) / 5;
        int numSmallCoin = coinAmount - numLargeCoin * 10 - numMediumCoin * 5;

        SpawnCoins(Coin.SmallCoin, numSmallCoin, position);
        SpawnCoins(Coin.MediumCoin, numMediumCoin, position);
        SpawnCoins(Coin.LargeCoin, numLargeCoin, position);
    }

    private void SpawnCoins(string type, int amount, Vector3 position)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector2 randPos = Random.insideUnitCircle;
            Vector3 spawnPos = position + new Vector3(randPos.x, 0, randPos.y);
            Instantiate(coinPrefabs[type], spawnPos, Quaternion.identity);
        }
    }
} 
