using UnityEngine;

public class EnemyLifeController : MonoBehaviour, IDamageable
{
    public EnemyDataSO EnemyData { get => enemyData; set => enemyData = value; }

    private int enemyLife;
    private EnemyDataSO enemyData;

    public void Init()
    {
        enemyLife = enemyData.maxHealth;
    }

    public bool IsDead()
    {
        return enemyLife <= 0;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 10)
            ParticlesManager.Instance.InstantiateSmallEnemyExplosion(transform.position);
        else
            ParticlesManager.Instance.InstantiateBigEnemyExplosion(transform.position);

        enemyLife -= damage;
    }
}
