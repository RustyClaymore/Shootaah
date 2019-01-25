using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetController : MonoBehaviour
{
    private EnemyDataSO enemyData;

    public EnemyDataSO EnemyData { set => enemyData = value; }

    public bool IsPlayerInChaseRange()
    {
        float distance = DistanceFromPlayer();
        return distance <= enemyData.chaseDistance && distance > enemyData.shootDistance;
    }

    public bool IsPlayerInAttackRange()
    {
        return DistanceFromPlayer() <= enemyData.shootDistance;
    }

    public bool IsPlayerOutOfRange()
    {
        return DistanceFromPlayer() > enemyData.chaseDistance;
    }

    private float DistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, SessionManager.Instance.CurrentPlayer.transform.position);
    }
}
