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
        Debug.Log("Enemy " + enemy.GetID() + " : Enetering shoot state !");
    }

    public override void Execute(EnemyEntity enemy)
    {
        Debug.Log("Enemy " + enemy.GetID() + " : Shooting at player !");

        // What should happen when he's roaming
        enemy.EnemyMovementController.LookAtPlayerTarget();
        enemy.EnemyShootController.Shot();

        // Check for condition to switch state
        if (enemy.EnemyTargetController.IsPlayerInChaseRange())
        {
            enemy.GetFSM().ChangeState(EnemyChaseState.Instance);
        }
        else if (enemy.EnemyTargetController.IsPlayerOutOfRange())
        {
            enemy.GetFSM().ChangeState(EnemyRoamState.Instance);
        }
    }

    public override void Exit(EnemyEntity enemy)
    {
        Debug.Log("Enemy " + enemy.GetID() + " : Enough shooting !");
    }
}