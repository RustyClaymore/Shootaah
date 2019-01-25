using UnityEngine;

public class PlayerLife : MonoBehaviour, IDamageable
{
    private PlayerDataSO playerData;
    private int playerHealth;
    private float regenCooldown;

    public PlayerDataSO PlayerData { get => playerData; set => playerData = value; }

    void Start()
    {
        playerHealth = playerData.maxHealth;
        regenCooldown = playerData.regenCooldown;
    }

    void Update()
    {
        if (!PlayerManager.Instance.PlayerMove.IsMoving())
        {
            regenCooldown -= Time.deltaTime;
            PeriodicHeal(playerData.healthRegen);
        }
        else
        {
            regenCooldown = playerData.regenCooldown;
        }

        if (IsDead())
        {
            SessionManager.Instance.FailMission();
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
    }

    public void Heal(int healAmount)
    {
        playerHealth = Mathf.Min(playerHealth + healAmount, playerData.maxHealth);
    }

    public bool IsDead()
    {
        return playerHealth <= 0;
    }
    
    private void PeriodicHeal(int healAmount)
    {
        if (playerHealth < playerData.maxHealth && regenCooldown <= 0)
        {
            Heal(healAmount);
            regenCooldown = playerData.regenCooldown;
        }
    }
}