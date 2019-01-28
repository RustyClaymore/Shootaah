using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField]
    private GameObject uiLevelManager;
    [SerializeField]
    private GameObject inputManager;
    [SerializeField]
    private GameObject sessionManager;
    [SerializeField]
    private GameObject wavesManager;
    [SerializeField]
    private GameObject collectibleSpawnManager;

    void Awake()
    {
        if (SessionManager.Instance == null)
        {
            Instantiate(sessionManager);
        }

        if (UILevelManager.Instance == null)
        {
            Instantiate(uiLevelManager);
        }

        if (InputManager.Instance == null)
        {
            Instantiate(inputManager);
        }

        if (EnemyWavesManager.Instance == null)
        {
            Instantiate(wavesManager);
        }

        if (CollectibleSpawnManager.Instance == null)
        {
            Instantiate(collectibleSpawnManager);
        }
    }
}