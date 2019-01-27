using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public PlayerMove PlayerMove { get => playerMove; }
    public PlayerShoot PlayerShot { get => playerShoot; }
    public PlayerLife PlayerLife { get => playerLife; }
    public PlayerTargetSystem TargetSystem { get => targetSystem; }
    public PlayerDataSO PlayerData { get => playerData; }
    public UpgradeLevels CurrentUpgradeLevels { get => currentUpgradeLevels; }
    public Transform[] Reactors { get => reactors; set => reactors = value; }

    [SerializeField]
    private PlayerDataSO playerData;
    [SerializeField]
    private GameObject defaultGun;
    [SerializeField]
    private Transform[] reactors;

    [SerializeField]
    private UpgradeLevels currentUpgradeLevels;

    private PlayerMove playerMove;
    private PlayerShoot playerShoot;
    private PlayerLife playerLife;
    private PlayerTargetSystem targetSystem;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentUpgradeLevels = PlayerUpgrades.LoadLevelsFromPlayerPrefs();

        targetSystem = gameObject.AddComponent<PlayerTargetSystem>() as PlayerTargetSystem;

        playerMove = gameObject.AddComponent<PlayerMove>() as PlayerMove;
        playerMove.Reactors = reactors;

        playerShoot = gameObject.AddComponent<PlayerShoot>() as PlayerShoot;
        GameObject currentGun = Instantiate(defaultGun, this.transform, false);
        playerShoot.CurrentGun = currentGun;

        playerLife = gameObject.AddComponent<PlayerLife>() as PlayerLife;
    }
}