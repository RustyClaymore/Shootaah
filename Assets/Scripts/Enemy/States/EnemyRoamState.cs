using UnityEngine;

public class EnemyRoamState : State<EnemyEntity>
{
    private static EnemyRoamState instance;

    private EnemyRoamState() { }

    public static EnemyRoamState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemyRoamState();
            }
            return instance;
        }
    }

    public override void Enter(EnemyEntity enemy)
    {
        Debug.Log("Enemy " + enemy.GetID() + " : Enetering roam state !");
        enemy.EnemyMovementController.IsMovingToNextTarget = false;
    }

    public override void Execute(EnemyEntity enemy)
    {
        Debug.Log("Enemy " + enemy.GetID() + " : Roaming !");

        // What should happen when he's roaming
        if (!enemy.EnemyMovementController.IsMovingToNextTarget || enemy.EnemyMovementController.HasReachedRoamTarget())
        {
            enemy.EnemyMovementController.SelectNextRoamTarget();
            enemy.EnemyMovementController.IsMovingToNextTarget = true;
        }

        // Check for condition to switch state
        if (enemy.EnemyTargetController.IsPlayerInChaseRange())
        {
            enemy.GetFSM().ChangeState(EnemyChaseState.Instance);
        }
        else if (enemy.EnemyTargetController.IsPlayerInAttackRange())
        {
            enemy.GetFSM().ChangeState(EnemyShootState.Instance);
        }
    }

    public override void FixedExecute(EnemyEntity enemy)
    {
        enemy.EnemyMovementController.LookAtNextRoamTarget();
        enemy.EnemyMovementController.MoveTowardsTarget();
    }

    public override void Exit(EnemyEntity enemy)
    {
        Debug.Log("Enemy " + enemy.GetID() + " : Enough roaming, time for some action !");
        enemy.EnemyMovementController.IsMovingToNextTarget = false;
    }
}