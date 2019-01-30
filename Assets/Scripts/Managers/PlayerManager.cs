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
    public GameObject OrientationArrow { get => orientationArrow; }

    [SerializeField]
    private PlayerDataSO playerData;
    [SerializeField]
    private GameObject[] defaultGuns;
    [SerializeField]
    private Transform[] reactors;
    [SerializeField]
    private Image healthBarImage;
    [SerializeField]
    private Image weaponJamBarImage;
    [SerializeField]
    private GameObject orientationArrow;


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

        // This is very dirty and is only used as a placeholder until a shop system for weapons is created
        // TODO: Create weapon shop system
        GameObject currentGun;
        if (PlayerPrefs.GetString("CurrentWeapon", "MissileLauncher") == "MissileLauncher")
        {
            currentGun = Instantiate(defaultGuns[0], this.transform, false);
        }
        else
        {
            currentGun = Instantiate(defaultGuns[1], this.transform, false);
        }
        playerShoot.CurrentGun = currentGun;

        playerLife = gameObject.AddComponent<PlayerLife>() as PlayerLife;
        collectibleCollector = gameObject.AddComponent<PlayerCollectibleCollector>() as PlayerCollectibleCollector;
    }
}