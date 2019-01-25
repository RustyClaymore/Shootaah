using UnityEngine;

public class EnemyLifeController : MonoBehaviour, IDamageable
{
    public EnemyDataSO EnemyData { get => enemyData; set => enemyData = value; }

    private int enemyLife;
    private EnemyDataSO enemyData;

    void Start()
    {
        enemyLife = enemyData.maxHealth;
    }

    public bool IsDead()
    {
        return enemyLife <= 0;
    }

    public void TakeDamage(int damage)
    {
        enemyLife -= damage;
    }
}
