using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerTargetSystem : MonoBehaviour
{
    public bool IsAiming { get; private set; }
    public GameObject TargetEnemy { get; private set; }

    public GameObject orientationArrow;

    private GameObject[] enemies;

    private struct EnemyDist
    {
        public GameObject enemy;
        public float distanceFromPlayer;
    }

    private void Start()
    {
        orientationArrow = PlayerManager.Instance.OrientationArrow;
    }

    // Update is called once per frame
    void Update()
    {
        enemies = EnemyWavesManager.Instance.GetCurrentEnemiesGOsArray();
     
        GameObject closestEnemy = GetClosestTargetableEnemy(enemies);
        
        if (closestEnemy != null)
        {
            IsAiming = true;
            TargetEnemy = closestEnemy;
        } else
        {
            IsAiming = false;
        }

        GameObject closestNonVisibleEnemy = GetClosestNonVisibleEnemy(enemies);

        if (closestNonVisibleEnemy != null)
        {
            orientationArrow.SetActive(true);
            Vector3 direction = (transform.position - closestNonVisibleEnemy.transform.position).normalized;
            orientationArrow.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            orientationArrow.SetActive(false);
        }
    }

    private GameObject GetClosestTargetableEnemy(GameObject[] enemies)
    {
        if (!enemies.Any())
            return null;

        List<EnemyDist> possibleTargets = new List<EnemyDist>();
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= PlayerManager.Instance.PlayerData.aimDistance.levelValues[PlayerManager.Instance.CurrentUpgradeLevels.aimDistance])
            {
                EnemyDist enemyDist = new EnemyDist();
                enemyDist.enemy = enemy;
                enemyDist.distanceFromPlayer = distance;
                possibleTargets.Add(enemyDist);
            }
        }

        if (!possibleTargets.Any())
            return null;

        return possibleTargets.OrderBy(e => e.distanceFromPlayer).First().enemy;
    }

    private GameObject GetClosestNonVisibleEnemy(GameObject[] enemies)
    {
        if (!enemies.Any())
            return null;

        List<EnemyDist> possibleTargets = new List<EnemyDist>();
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponentInChildren<Renderer>().isVisible)
            {
                return null;
            }
            else
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                EnemyDist enemyDist = new EnemyDist();
                enemyDist.enemy = enemy;
                enemyDist.distanceFromPlayer = distance;
                possibleTargets.Add(enemyDist);
            }
        }
        
        if (!possibleTargets.Any())
            return null;

        return possibleTargets.OrderBy(e => e.distanceFromPlayer).First().enemy;
    }
}
