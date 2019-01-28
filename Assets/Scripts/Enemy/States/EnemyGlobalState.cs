using UnityEngine;

public class EnemyGlobalState : State<EnemyEntity>
{
    private static EnemyGlobalState instance;

    private EnemyGlobalState() { }

    public static EnemyGlobalState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemyGlobalState();
            }
            return instance;
        }
    }

    public override void Execute(EnemyEntity enemy)
    {
        if (enemy.EnemyMovementController.HasImpactedPlayer)
        {
            enemy.EnemyLifeController.TakeDamage(enemy.EnemyData.maxHealth);
        }

        if(enemy.EnemyLifeController.IsDead())
        {
            EnemyWavesManager.Instance.Kill(enemy);
        }
    }
}