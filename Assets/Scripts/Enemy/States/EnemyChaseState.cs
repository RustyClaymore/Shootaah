using UnityEngine;

public class EnemyChaseState : State<EnemyEntity>
{
    private static EnemyChaseState instance;

    private EnemyChaseState() { }

    public static EnemyChaseState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemyChaseState();
            }
            return instance;
        }
    }

    public override void Enter(EnemyEntity enemy)
    {
    }

    public override void Execute(EnemyEntity enemy)
    {
        if (enemy.EnemyTargetController.IsPlayerInAttackRange() && enemy.GetEntityType() == (int)EntityType.enemyFighterType)
        {
            enemy.GetFSM().ChangeState(EnemyShootState.Instance);
        }
        else if (enemy.EnemyTargetController.IsPlayerOutOfRange())
        {
            enemy.GetFSM().ChangeState(EnemyRoamState.Instance);
        }
    }

    public override void FixedExecute(EnemyEntity enemy)
    {
        enemy.EnemyMovementController.LookAtPlayerTarget();
        enemy.EnemyMovementController.MoveTowardsTarget();
    }

    public override void Exit(EnemyEntity enemy)
    {
    }
}
