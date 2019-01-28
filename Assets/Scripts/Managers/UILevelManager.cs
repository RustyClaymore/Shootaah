using UnityEngine;
using UnityEngine.UI;

public class UILevelManager : MonoBehaviour
{
    public static UILevelManager Instance { get; private set; }

    public Image PlayerHealthBar { get => playerHealthBar; set => playerHealthBar = value; }
    public Image PlayerWeaponJamBar { get => playerWeaponJamBar; set => playerWeaponJamBar = value; }
    public Text CoinsText { get => coinsText; set => coinsText = value; }
    
    [SerializeField]
    private Text coinsText;

    private Image playerHealthBar;
    private Image playerWeaponJamBar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar = PlayerManager.Instance.HealthBarImage;
        playerWeaponJamBar = PlayerManager.Instance.WeaponJamBarImage;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Instance)
        {
            playerHealthBar.fillAmount = PlayerManager.Instance.PlayerLife.CurrentHealth / 100f;
            playerWeaponJamBar.fillAmount = PlayerManager.Instance.PlayerShoot.CurrentGun.GetComponent<PlayerGun>().CurrentJamRate / 100f;
        }

        if (SessionManager.Instance)
        {
            coinsText.text = "Coins : " + SessionManager.Instance.CollectedCoinAmount;
        }
    }
}
