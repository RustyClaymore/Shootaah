using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerDataSO playerData;
    [SerializeField]
    private GameObject defaultGun;

    private PlayerMove playerMove;
    private PlayerShoot playerShoot;
    private PlayerLife playerLife;
    private PlayerTargetSystem targetSystem;

    public static PlayerManager Instance { get; private set; }

    public PlayerMove PlayerMove { get => playerMove; set => playerMove = value; }
    public PlayerShoot PlayerShot { get => playerShoot; set => playerShoot = value; }
    public PlayerLife PlayerLife { get => playerLife; set => playerLife = value; }
    public PlayerTargetSystem TargetSystem { get => targetSystem; set => targetSystem = value; }

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

        targetSystem = gameObject.AddComponent<PlayerTargetSystem>() as PlayerTargetSystem;
        targetSystem.PlayerData = playerData;

        playerMove = gameObject.AddComponent<PlayerMove>() as PlayerMove;
        playerMove.PlayerData = playerData;

        playerShoot = gameObject.AddComponent<PlayerShoot>() as PlayerShoot;
        playerShoot.CurrentGun = defaultGun;

        playerLife = gameObject.AddComponent<PlayerLife>() as PlayerLife;
        playerLife.PlayerData = playerData;
    }
}