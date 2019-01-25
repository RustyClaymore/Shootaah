using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance { get; private set; }
    public GameObject CurrentPlayer { get => currentPlayer; }
    public bool IsGameStarted { get => isGameStarted; }
    public bool IsGamePaused { get => isGamePaused; }
    public bool MissionCompleted { get => missionCompleted; }
    public bool MissionFailed { get => missionFailed; }

    [SerializeField]
    private GameObject playerPrefab;
    private GameObject currentPlayer;

    private bool isGamePaused;
    private bool isGameStarted;
    private bool missionCompleted;
    private bool missionFailed;

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
        missionCompleted = false;
        missionFailed = false;
    }

    void Update()
    {
        if (missionCompleted)
        {
            Debug.Log("Mission Accomplished");
        }

        if (missionFailed)
        {
            Debug.Log("Mission Failed");
        }
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