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
        Debug.Log("Enemy " + enemy.GetID() + " : Enetering chase state !");
    }

    public override void Execute(EnemyEntity enemy)
    {
        Debug.Log("Enemy " + enemy.GetID() + " : Chasing player !");

        // What should happen when he's roaming

        // Check for condition to switch state
        if (enemy.EnemyTargetController.IsPlayerInAttackRange())
        {
            Debug.Log("Should be attacking player");
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
        Debug.Log("Enemy " + enemy.GetID() + " : Quitting chase state, can't catch him !");
    }
}
