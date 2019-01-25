using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private GameObject inputManager;
    [SerializeField]
    private GameObject sessionManager;

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
    }
}