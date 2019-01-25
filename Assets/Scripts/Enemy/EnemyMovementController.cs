﻿using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    public EnemyDataSO EnemyData { get => enemyData; set => enemyData = value; }
    public bool IsMovingToNextTarget { get => isMovingToNextRoamTarget; set => isMovingToNextRoamTarget = value; }
    public Vector3 NextRoamTarget { get => nextRoamTarget; set => nextRoamTarget = value; }
    public bool IsMovingToNextRoamTarget { get => isMovingToNextRoamTarget; set => isMovingToNextRoamTarget = value; }

    private new Rigidbody rigidbody;
    private EnemyDataSO enemyData;

    private bool isMovingToNextRoamTarget;
    private Vector3 nextRoamTarget;

    public void Init()
    {
        rigidbody = GetComponent<Rigidbody>();

        isMovingToNextRoamTarget = false;
        nextRoamTarget = Vector3.zero;
    }

    public void MoveTowardsTarget()
    {
        rigidbody.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * enemyData.speed);
    }

    public void LookAtPlayerTarget()
    {
        Vector3 targetDirection = SessionManager.Instance.CurrentPlayer.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = rotation;
    }
    
    public void LookAtNextRoamTarget()
    {
        Vector3 targetDirection = nextRoamTarget - transform.position;
        Quaternion rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = rotation;
    }

    public void SelectNextRoamTarget()
    {
        nextRoamTarget = Random.insideUnitSphere * enemyData.roamRadius;
        nextRoamTarget.y = 0;
    }

    public bool HasReachedRoamTarget()
    {
        return Vector3.Distance(transform.position, nextRoamTarget) <= 1;
    }
}
