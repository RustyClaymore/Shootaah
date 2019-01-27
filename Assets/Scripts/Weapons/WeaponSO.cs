using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab;
    public GunDataSO gunData;

    public string description;
    public Sprite weaponIcon;
}
