using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerTargetSystem : MonoBehaviour
{
    public bool IsAiming { get; private set; }
    public GameObject TargetEnemy { get; private set; }

    private GameObject[] enemies;

    private struct EnemyDist
    {
        public GameObject enemy;
        public float distanceFromPlayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject closestEnemy = GetClosestEnemy(enemies);
        
        if (closestEnemy != null)
        {
            IsAiming = true;
            TargetEnemy = closestEnemy;
        } else
        {
            IsAiming = false;
        }
    }

    private GameObject GetClosestEnemy(GameObject[] enemies)
    {
        if (!enemies.Any())
            return null;

        List<EnemyDist> possibleTargets = new List<EnemyDist>();
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= 10)
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
}
