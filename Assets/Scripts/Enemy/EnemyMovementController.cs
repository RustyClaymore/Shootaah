using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    public EnemyDataSO EnemyData { get => enemyData; set => enemyData = value; }
    public bool IsMovingToNextTarget { get => isMovingToNextRoamTarget; set => isMovingToNextRoamTarget = value; }
    public Vector3 NextRoamTarget { get => nextRoamTarget; set => nextRoamTarget = value; }
    public bool IsMovingToNextRoamTarget { get => isMovingToNextRoamTarget; set => isMovingToNextRoamTarget = value; }
    public bool HasImpactedPlayer { get => hasImpactedPlayer; }

    private new Rigidbody rigidbody;
    private EnemyDataSO enemyData;

    private bool isMovingToNextRoamTarget;
    private Vector3 nextRoamTarget;

    private bool hasImpactedPlayer;

    public void Init()
    {
        rigidbody = GetComponent<Rigidbody>();

        isMovingToNextRoamTarget = false;
        nextRoamTarget = Vector3.zero;

        hasImpactedPlayer = false;
    }
    
    public void MoveTowardsTarget()
    {
        rigidbody.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * enemyData.speed);
    }

    public void LookAtPlayerTarget()
    {
        Vector3 targetDirection = SessionManager.Instance.CurrentPlayer.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.1f);
    }

    public void LookAtNextRoamTarget()
    {
        Vector3 targetDirection = nextRoamTarget - transform.position;
        Quaternion rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.1f);
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

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable is PlayerLife)
        {
            damageable.TakeDamage(enemyData.impactDamage);
            hasImpactedPlayer = true;
        }
    }
}
