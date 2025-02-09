﻿using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    public static ParticlesManager Instance { get; private set; }

    public float explosionDuration;

    [Header("Player Particles")]
    public GameObject playerDeathExplosion;
    public GameObject smallPlayerExplosion;
    public GameObject bigPlayerExplosion;

    [Header("Enemy Particles")]
    public GameObject enemySpawnParticle;
    public GameObject smallEnemyExplosion;
    public GameObject bigEnemyExplosion;

    [Header("Diamond Blimps")]
    public GameObject smallDiamondBlimp;
    public GameObject mediumDiamondBlimp;
    public GameObject largeDiamondBlimp;

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
    }

    public void InstantiatePlayerDeathExplosion(Vector3 position)
    {
        GameObject explosion = Instantiate(playerDeathExplosion, position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
    }

    public void InstantiateEnemySpawnParticle(Vector3 position, float duration)
    {
        GameObject spawnParticle = Instantiate(enemySpawnParticle, position, Quaternion.identity);
        Destroy(spawnParticle, duration);
    }

    public void InstantiateSmallPlayerExplosion(Vector3 position)
    {
        GameObject explosion = Instantiate(smallPlayerExplosion, position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
    }

    public void InstantiateBigPlayerExplosion(Vector3 position)
    {
        GameObject explosion = Instantiate(bigPlayerExplosion, position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
    }

    public void InstantiateSmallEnemyExplosion(Vector3 position)
    {
        GameObject explosion = Instantiate(smallEnemyExplosion, position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
    }

    public void InstantiateBigEnemyExplosion(Vector3 position)
    {
        GameObject explosion = Instantiate(bigEnemyExplosion, position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
    }

    public void IntantiateDiamondBlimp(Vector3 position, int diamondAmount)
    {
        GameObject diamondBlimp;
        Vector3 randPos = position + Random.insideUnitSphere * 5;
        if (diamondAmount == 1)
            diamondBlimp = Instantiate(smallDiamondBlimp, randPos, Quaternion.identity);
        else if (diamondAmount == 5)
            diamondBlimp = Instantiate(mediumDiamondBlimp, randPos, Quaternion.identity);
        else
            diamondBlimp = Instantiate(largeDiamondBlimp, randPos, Quaternion.identity);
        Destroy(diamondBlimp, 2);
    }
}
