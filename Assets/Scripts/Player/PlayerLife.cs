using UnityEngine;

public class PlayerLife : MonoBehaviour, IDamageable
{
    private int playerMaxHealth;
    private int healthRegen;
    private float regenCooldown;
    
    [SerializeField]
    private int currentHealth;
    private float currentRegenCooldown;

    void Start()
    {
        PlayerDataSO playerData = PlayerManager.Instance.PlayerData;
        UpgradeLevels currentUpgradeLevels = PlayerManager.Instance.CurrentUpgradeLevels;

        playerMaxHealth = playerData.maxHealth[currentUpgradeLevels.maxHealth];
        currentHealth = playerMaxHealth;

        regenCooldown = playerData.regenCooldown[currentUpgradeLevels.regenCooldown];
        currentRegenCooldown = regenCooldown;

        healthRegen = playerData.healthRegen[currentUpgradeLevels.healthRegen];
    }

    void Update()
    {
        if (!PlayerManager.Instance.PlayerMove.IsMoving())
        {
            currentRegenCooldown -= Time.deltaTime;
            PeriodicHeal(healthRegen);
        }
        else
        {
            currentRegenCooldown = regenCooldown;
        }

        if (IsDead())
        {
            SessionManager.Instance.FailMission();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, playerMaxHealth);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
    
    private void PeriodicHeal(int healAmount)
    {
        if (currentHealth < playerMaxHealth && currentRegenCooldown <= 0)
        {
            Heal(healAmount);
            currentRegenCooldown = regenCooldown;
        }
    }
}