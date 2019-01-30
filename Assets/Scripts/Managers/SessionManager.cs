using UnityEngine;
using System.Collections;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance { get; private set; }
    public GameObject CurrentPlayer { get => currentPlayer; }
    public bool IsGameStarted { get => isGameStarted; }
    public bool IsGamePaused { get => isGamePaused; }
    public bool IsGameEnded { get => isGameEnded; }
    public bool MissionCompleted { get => missionCompleted; }
    public bool MissionFailed { get => missionFailed; }
    public int CollectedDiamondAmount { get => collectedDiamondAmount; }

    [SerializeField]
    private GameObject playerPrefab;
    private GameObject currentPlayer;

    private bool isGamePaused;
    private bool isGameStarted;
    private bool isGameEnded;
    private bool missionCompleted;
    private bool missionFailed;

    private int collectedDiamondAmount;
    private bool diamondsSaved = false;

    private float countDownBeforeEndScreen;

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

        currentPlayer = Instantiate(playerPrefab, new Vector3(0, 0, -5), Quaternion.identity) as GameObject;

        isGamePaused = false;
        isGameStarted = true;
        isGameEnded = false;

        missionCompleted = false;
        missionFailed = false;

        collectedDiamondAmount = 0;
        diamondsSaved = false;

        countDownBeforeEndScreen = 2;
    }

    void Update()
    {
        if (missionCompleted)
        {
            isGameEnded = true;

            isGamePaused = true;
            
            countDownBeforeEndScreen -= Time.deltaTime;
            if (countDownBeforeEndScreen <= 0)
            {
                UILevelManager.Instance.ActivateWinScreenUI();
                SaveCollectedDiamondAmount();
            }
        }

        if (missionFailed)
        {
            isGameEnded = true;

            isGamePaused = true;
            
            countDownBeforeEndScreen -= Time.deltaTime;
            if (countDownBeforeEndScreen <= 0)
            {
                collectedDiamondAmount = 0;

                UILevelManager.Instance.ActivateLoseScreenUI();
            }
        }
    }

    public void IncreaseDiamondAmount(int amount)
    {
        collectedDiamondAmount += amount;
    }

    public void SaveCollectedDiamondAmount()
    {
        if (diamondsSaved)
        {
            return;
        }
        PlayerPrefs.SetInt(Diamond.DiamondType, Diamond.GetCurrentDiamondAmount() + collectedDiamondAmount);
        diamondsSaved = true;
    }

    public void CompleteMission()
    {
        missionCompleted = true;
    }

    public void FailMission()
    {
        missionFailed = true;
    }

    void StartGame()
    {
        isGameStarted = true;
    }

    void PauseGame()
    {
        isGamePaused = true;
    }

    void UnpauseGame()
    {
        isGamePaused = false;
    }
}