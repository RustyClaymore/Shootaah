using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class HomingMissileTargetSystem : MonoBehaviour
{
    public Transform TargetEnemy { get; private set; }
    public float aimDistance;

    private GameObject[] enemies;

    private struct EnemyDist
    {
        public GameObject enemy;
        public float distanceFromProjectile;
    }

    // Update is called once per frame
    void Update()
    {
        enemies = EnemyWavesManager.Instance.GetCurrentEnemiesGOsArray();
     
        Transform closestEnemy = GetClosestTargetableEnemy(enemies);
        
        if (closestEnemy != null)
        {
            TargetEnemy = closestEnemy;
        }
    }

    private Transform GetClosestTargetableEnemy(GameObject[] enemies)
    {
        if (!enemies.Any())
            return null;

        List<EnemyDist> possibleTargets = new List<EnemyDist>();
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= aimDistance)
            {
                EnemyDist enemyDist = new EnemyDist();
                enemyDist.enemy = enemy;
                enemyDist.distanceFromProjectile = distance;
                possibleTargets.Add(enemyDist);
            }
        }

        if (!possibleTargets.Any())
            return null;

        return possibleTargets.OrderBy(e => e.distanceFromProjectile).First().enemy.transform;
    }
}
