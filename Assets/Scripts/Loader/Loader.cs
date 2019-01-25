using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private GameObject inputManager;
    [SerializeField]
    private GameObject sessionManager;
    [SerializeField]
    private GameObject wavesManager;

    void Awake()
    {
        if (InputManager.Instance == null)
        {
            Instantiate(inputManager);
        }

        if (SessionManager.Instance == null)
        {
            Instantiate(sessionManager);
        }

        if (EnemyWavesManager.Instance == null)
        {
            Instantiate(wavesManager);
        }
    }
}