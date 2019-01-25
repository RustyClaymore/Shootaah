using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerDataSO : ScriptableObject
{
    public int maxHealth;
    public int healthRegen;
    public float regenCooldown;

    public float speed;

    public float aimDistance;
}