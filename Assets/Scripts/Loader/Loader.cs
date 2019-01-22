using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private GameObject inputManager;

    void Awake()
    {
        if (InputManager.Instance == null)
        {
            Instantiate(inputManager);
        }
    }
}