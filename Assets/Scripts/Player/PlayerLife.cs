using UnityEngine;

public class PlayerLife : MonoBehaviour, IDamageable
{
    public int CurrentHealth { get => currentHealth; }
    public GameObject SmallExplosion { get => smallExplosion; set => smallExplosion = value; }
    public GameObject BigExplosion { get => bigExplosion; set => bigExplosion = value; }

    private int playerMaxHealth;
    private int healthRegen;
    private float regenCooldown;

    private int currentHealth;
    private float currentRegenCooldown;

    private GameObject smallExplosion;
    private GameObject bigExplosion;

    void Start()
    {
        PlayerDataSO playerData = PlayerManager.Instance.PlayerData;
        UpgradeLevels currentUpgradeLevels = PlayerManager.Instance.CurrentUpgradeLevels;

        playerMaxHealth = (int)playerData.maxHealth.levelValues[currentUpgradeLevels.maxHealth];
        currentHealth = playerMaxHealth;

        regenCooldown = playerData.regenCooldown.levelValues[currentUpgradeLevels.regenCooldown];
        currentRegenCooldown = regenCooldown;

        healthRegen = (int)playerData.healthRegen.levelValues[currentUpgradeLevels.healthRegen];
    }

    void Update()
    {
        if (SessionManager.Instance.IsGamePaused || !SessionManager.Instance.IsGameStarted)
        {
            return;
        }

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
            ParticlesManager.Instance.InstantiatePlayerDeathExplosion(transform.position);
            int children = transform.childCount;
            for (int i = 0; i < children; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 15)
            ParticlesManager.Instance.InstantiateSmallPlayerExplosion(transform.position);
        else
            ParticlesManager.Instance.InstantiateBigPlayerExplosion(transform.position);

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