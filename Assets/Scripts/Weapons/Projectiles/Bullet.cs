using UnityEngine;

public class Bullet : MonoBehaviour, IProjectile
{
    [SerializeField]
    private ProjectileDataSO bulletData;

    void OnTriggerEnter(Collider collider)
    {
        IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(bulletData.damage);
        }

        Destroy(this.gameObject);
    }

    public int GetDamage()
    {
        return bulletData.damage;
    }
}