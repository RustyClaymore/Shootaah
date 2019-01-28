using UnityEngine;

[System.Serializable]
public struct Upgrade
{
    public string name;
    public string description;
    public float[] levelValues;
    public int[] levelUpgradeCosts;
}

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerDataSO : ScriptableObject
{
    public Upgrade maxHealth;
    public Upgrade healthRegen;
    public Upgrade regenCooldown;
    public Upgrade speed;
    public Upgrade aimDistance;
}