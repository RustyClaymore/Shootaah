using UnityEngine;

public class EnemyEntity : BaseGameEntity
{
    public EnemyMovementController EnemyMovementController { get => enemyMovementController; }
    public EnemyTargetController EnemyTargetController { get => enemyTargetController; }
    public EnemyShootController EnemyShootController { get => enemyShootController; }
    public EnemyLifeController EnemyLifeController { get => enemyLifeController; }
    public GameObject EnemyGO { get => enemyGO; }
    public EnemyDataSO EnemyData { get => enemyData; }

    private GameObject enemyGO;
    private EnemyDataSO enemyData;

    private EnemyLifeController enemyLifeController;
    private EnemyMovementController enemyMovementController;
    private EnemyTargetController enemyTargetController;
    private EnemyShootController enemyShootController;

    private StateMachine<EnemyEntity> stateMachine;

    public EnemyEntity(
        int id,
        int enemyType,
        GameObject enemyGO,
        EnemyDataSO enemyData,
        GameObject currentGun
    ) : base(id, enemyType)
    {
        stateMachine = new StateMachine<EnemyEntity>(this);
        if (enemyType == (int)EntityType.enemyFighterType)
        {
            stateMachine.SetCurrentState(EnemyRoamState.Instance);
        }
        else if (enemyType == (int)EntityType.enemyKamikazeType)
        {
            stateMachine.SetCurrentState(EnemyChaseState.Instance);
        }
        stateMachine.SetGlobalState(EnemyGlobalState.Instance);

        this.enemyGO = enemyGO;
        this.enemyData = enemyData;

        enemyLifeController = enemyGO.AddComponent<EnemyLifeController>();
        enemyLifeController.EnemyData = enemyData;
        enemyLifeController.Init();

        enemyMovementController = enemyGO.AddComponent<EnemyMovementController>();
        enemyMovementController.EnemyData = enemyData;
        enemyMovementController.Init();

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
