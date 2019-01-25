using UnityEngine;

public class EnemyEntity : BaseGameEntity
{
    public EnemyMovementController EnemyMovementController { get => enemyMovementController; }
    public EnemyTargetController EnemyTargetController { get => enemyTargetController; }
    public EnemyShootController EnemyShootController { get => enemyShootController; }

    private GameObject enemyGO;
    private EnemyDataSO enemyData;

    private EnemyMovementController enemyMovementController;
    private EnemyTargetController enemyTargetController;
    private EnemyShootController enemyShootController;

    private StateMachine<EnemyEntity> stateMachine;

    public EnemyEntity(int id, GameObject enemyGO, EnemyDataSO enemyData, GameObject currentGun) : base(id, (int)EntityType.enemyEntityType)
    {
        stateMachine = new StateMachine<EnemyEntity>(this);
        stateMachine.SetCurrentState(EnemyRoamState.Instance);
        stateMachine.SetGlobalState(EnemyGlobalState.Instance);

        this.enemyGO = enemyGO;
        this.enemyData = enemyData;

        enemyMovementController = enemyGO.AddComponent<EnemyMovementController>();
        enemyMovementController.EnemyData = enemyData;

        enemyTargetController = enemyGO.AddComponent<EnemyTargetController>();
        enemyTargetController.EnemyData = enemyData;

        enemyShootController = enemyGO.AddComponent<EnemyShootController>();
        enemyShootController.CurrentGun = currentGun;
    }

    public override void Update()
    {
        stateMachine.Update();
    }
    
    public override bool HandleMessage()
    {
        return base.HandleMessage();
    }

    public StateMachine<EnemyEntity> GetFSM() { return stateMachine; }
}
