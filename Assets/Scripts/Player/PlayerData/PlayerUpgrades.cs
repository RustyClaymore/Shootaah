using UnityEngine;

[System.Serializable]
public struct UpgradeLevels
{
    public int maxHealth;
    public int healthRegen;
    public int regenCooldown;
    public int speed;
    public int aimDistance;
}

public static class PlayerUpgrades
{
    public const string MaxHealth = "MaxHealth";
    public const string HealthRegen = "HealthRegen";
    public const string RegenCooldown = "RegenCooldown";
    public const string Speed = "Speed";
    public const string AimDistance = "AimDistance";

    private static UpgradeLevels levels;


    public static UpgradeLevels LoadLevelsFromPlayerPrefs()
    {
        levels.maxHealth = PlayerPrefs.GetInt(MaxHealth, 0);
        levels.healthRegen = PlayerPrefs.GetInt(HealthRegen, 0);
        levels.regenCooldown = PlayerPrefs.GetInt(RegenCooldown, 0);
        levels.speed = PlayerPrefs.GetInt(Speed, 0);
        levels.aimDistance = PlayerPrefs.GetInt(AimDistance, 0);

        return levels;
    }

    public static void SaveLevelsToPlayerPrefs()
    {
        PlayerPrefs.SetInt(MaxHealth, levels.maxHealth);
        PlayerPrefs.SetInt(HealthRegen, levels.healthRegen);
        PlayerPrefs.SetInt(RegenCooldown, levels.regenCooldown);
        PlayerPrefs.SetInt(Speed, levels.speed);
        PlayerPrefs.SetInt(AimDistance, levels.aimDistance);
    }

    public static int GetUpgradeLevel(string upgrade)
    {
        return PlayerPrefs.GetInt(upgrade, 0);
    }

    public static void Upgrade(string upgrade)
    {
        PlayerPrefs.SetInt(upgrade, GetUpgradeLevel(upgrade) + 1);
    }

    public static void ResetPlayerUpgrades()
    {
        PlayerPrefs.SetInt(MaxHealth, 0);
        PlayerPrefs.SetInt(HealthRegen, 0);
        PlayerPrefs.SetInt(RegenCooldown, 0);
        PlayerPrefs.SetInt(Speed, 0);
        PlayerPrefs.SetInt(AimDistance, 0);
    }
}