using UnityEngine;

public class EnemyShootState : State<EnemyEntity>
{
    private static EnemyShootState instance;

    private EnemyShootState() { }

    public static EnemyShootState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemyShootState();
            }
            return instance;
        }
    }

    public override void Enter(EnemyEntity enemy)
    {
    }

    public override void Execute(EnemyEntity enemy)
    {
        enemy.EnemyMovementController.LookAtPlayerTarget();
        enemy.EnemyShootController.Shot();
        
        if (enemy.EnemyTargetController.IsPlayerInChaseRange())
        {
            enemy.GetFSM().ChangeState(EnemyChaseState.Instance);
        }
        else if (enemy.EnemyTargetController.IsPlayerOutOfRange())
        {
            enemy.GetFSM().ChangeState(EnemyRoamState.Instance);
        }
    }
    
    public override void FixedExecute(EnemyEntity enemy)
    {
        if (enemy.EnemyTargetController.IsPlayerInChaseShootRange())
        {
            enemy.EnemyMovementController.LookAtPlayerTarget();
            enemy.EnemyMovementController.MoveTowardsTarget();
        }
    }

    public override void Exit(EnemyEntity enemy)
    {
    }
}