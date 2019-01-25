using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    private GameObject currentPlayer;

    public static SessionManager Instance { get; private set; }
    public GameObject CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

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

        currentPlayer = Instantiate(playerPrefab) as GameObject;
    }
}