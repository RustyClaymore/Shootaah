using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField]
    private GameObject uiLevelManager;
    [SerializeField]
    private GameObject mobileInputPrefab;
    [SerializeField]
    private GameObject inputManager;
    [SerializeField]
    private GameObject sessionManager;
    [SerializeField]
    private GameObject wavesManager;
    [SerializeField]
    private GameObject collectibleSpawnManager;
    [SerializeField]
    private GameObject particlesManager;

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

#if UNITY_ANDROID
        Instantiate(mobileInputPrefab);
#endif

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

        if (ParticlesManager.Instance == null)
        {
            Instantiate(particlesManager);
        }
    }
}