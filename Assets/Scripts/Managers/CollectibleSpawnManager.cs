using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawnManager : MonoBehaviour
{
    public static CollectibleSpawnManager Instance { get; private set; }

    public GameObject smallDiamondPrefab;
    public GameObject mediumDiamondPrefab;
    public GameObject largeDiamondPrefab;

    private Dictionary<string, GameObject> diamondPrefabs;

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

        diamondPrefabs = new Dictionary<string, GameObject>();
        diamondPrefabs[Diamond.SmallDiamond] = smallDiamondPrefab;
        diamondPrefabs[Diamond.MediumDiamond] = mediumDiamondPrefab;
        diamondPrefabs[Diamond.LargeDiamond] = largeDiamondPrefab;
    }

    public void SpawnDiamondsAtPosition(int coinAmount, Vector3 position)
    {
        int numLargeCoin = coinAmount / 10;
        int numMediumCoin = (coinAmount - 10 * numLargeCoin) / 5;
        int numSmallCoin = coinAmount - numLargeCoin * 10 - numMediumCoin * 5;

        SpawnDiamonds(Diamond.SmallDiamond, numSmallCoin, position);
        SpawnDiamonds(Diamond.MediumDiamond, numMediumCoin, position);
        SpawnDiamonds(Diamond.LargeDiamond, numLargeCoin, position);
    }

    private void SpawnDiamonds(string type, int amount, Vector3 position)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector2 randPos = Random.insideUnitCircle;
            Vector3 spawnPos = position + new Vector3(randPos.x, 0, randPos.y);
            Instantiate(diamondPrefabs[type], spawnPos, Quaternion.identity);
        }
    }
} 
