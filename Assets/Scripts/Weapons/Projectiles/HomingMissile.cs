using UnityEngine;

public class HomingMissile : MonoBehaviour, IProjectile
{
    [SerializeField]
    private ProjectileDataSO missileData;

    private float currentHomingSpeed;
    private Transform target;

    void OnTriggerEnter(Collider collider)
    {
        IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(missileData.damage);
        }

        Destroy(this.gameObject);
    }

    public void Update()
    {
        LockOnTarget();

        currentHomingSpeed += Time.deltaTime * 30;
        currentHomingSpeed = Mathf.Min(currentHomingSpeed, missileData.maxHomingSpeed);
        ChaseTarget();
    }

    public void LockOnTarget()
    {
        target = GetComponent<HomingMissileTargetSystem>().TargetEnemy;
        if (target != null)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position, Vector3.up), 0.08f);
        }
    }

    public void ChaseTarget()
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * currentHomingSpeed);
    }
    
    public int GetDamage()
    {
        return missileData.damage;
    }
}
