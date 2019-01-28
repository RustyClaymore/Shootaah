using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public PlayerMove PlayerMove { get => playerMove; }
    public PlayerShoot PlayerShoot { get => playerShoot; }
    public PlayerLife PlayerLife { get => playerLife; }
    public PlayerTargetSystem TargetSystem { get => targetSystem; }
    public PlayerCollectibleCollector CollectibleCollector { get => collectibleCollector; }

    public PlayerDataSO PlayerData { get => playerData; }
    public UpgradeLevels CurrentUpgradeLevels { get => currentUpgradeLevels; }
    public Transform[] Reactors { get => reactors; set => reactors = value; }
    public Image HealthBarImage { get => healthBarImage;  }
    public Image WeaponJamBarImage { get => weaponJamBarImage; }

    [SerializeField]
    private PlayerDataSO playerData;
    [SerializeField]
    private GameObject defaultGun;
    [SerializeField]
    private Transform[] reactors;
    [SerializeField]
    private Image healthBarImage;
    [SerializeField]
    private Image weaponJamBarImage;


    [SerializeField]
    private UpgradeLevels currentUpgradeLevels;

    private PlayerMove playerMove;
    private PlayerShoot playerShoot;
    private PlayerLife playerLife;
    private PlayerTargetSystem targetSystem;
    private PlayerCollectibleCollector collectibleCollector;

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

        collectibleCollector = gameObject.AddComponent<PlayerCollectibleCollector>() as PlayerCollectibleCollector;
    }
}