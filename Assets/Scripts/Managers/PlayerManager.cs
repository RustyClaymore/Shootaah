using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public PlayerMove PlayerMove { get => playerMove; }
    public PlayerShoot PlayerShot { get => playerShoot; }
    public PlayerLife PlayerLife { get => playerLife; }
    public PlayerTargetSystem TargetSystem { get => targetSystem; }

    [SerializeField]
    private PlayerDataSO playerData;
    [SerializeField]
    private GameObject defaultGun;

    private PlayerMove playerMove;
    private PlayerShoot playerShoot;
    private PlayerLife playerLife;
    private PlayerTargetSystem targetSystem;

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
        GameObject currentGun = Instantiate(defaultGun, this.transform, false);
        playerShoot.CurrentGun = currentGun;

        playerLife = gameObject.AddComponent<PlayerLife>() as PlayerLife;
        playerLife.PlayerData = playerData;
    }
}