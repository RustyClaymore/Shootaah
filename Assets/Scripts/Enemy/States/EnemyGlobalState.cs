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
        /*
        if (enemy.IsFleeing && !enemy.IsAttacking && (enemy.GetFSM().GetCurrentState() != KnightEscapeEnemiesState.Instance))
        {
            enemy.GetFSM().ChangeState(KnightEscapeEnemiesState.Instance);
        }

        if (enemy.IsAttacking && (enemy.GetFSM().GetCurrentState() != KnightChaseEnemyState.Instance) && (enemy.GetFSM().GetCurrentState() != KnightAttackEnemyState.Instance))
        {
            enemy.GetFSM().ChangeState(KnightChaseEnemyState.Instance);
        }*/
    }
}