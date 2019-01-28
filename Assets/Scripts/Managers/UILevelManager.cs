using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelManager : MonoBehaviour
{
    public static UILevelManager Instance { get; private set; }

    public int nextLevelIndex = -1;

    [Header("In Game")]
    public Text diamondsText;
    [Header("End Screen")]
    public GameObject logo;
    public GameObject replayButton;
    public GameObject mainMenuButton;
    [Header("Win Screen")]
    public GameObject missionComplete;
    public GameObject objectifMedal1;
    public GameObject objectifMedal2;
    public GameObject objectifMedal3;
    public GameObject nextMissionButton;
    [Header("Lose Screen")]
    public GameObject missionFailed;

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
        DeactivateEndScreenUI();

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
            diamondsText.text = SessionManager.Instance.CollectedDiamondAmount.ToString();
        }
    }
    
    private void DeactivateEndScreenUI()
    {
        missionComplete.SetActive(false);
        objectifMedal1.SetActive(false);
        objectifMedal2.SetActive(false);
        objectifMedal3.SetActive(false);
        nextMissionButton.SetActive(false);
        missionFailed.SetActive(false);
        logo.SetActive(false);
        replayButton.SetActive(false);
        mainMenuButton.SetActive(false);
    }

    public void ActivateWinScreenUI()
    {
        ActivateEndScreenUI();
        missionComplete.SetActive(true);
        objectifMedal1.SetActive(true);
        objectifMedal2.SetActive(true);
        objectifMedal3.SetActive(true);
        nextMissionButton.SetActive(nextLevelIndex != -1);
    }

    public void ActivateLoseScreenUI()
    {
        ActivateEndScreenUI();
        missionFailed.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelIndex);
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ActivateEndScreenUI()
    {
        logo.SetActive(true);
        replayButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }
}
