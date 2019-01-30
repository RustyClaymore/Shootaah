using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/ProjectileData", order = 1)]
public class ProjectileDataSO : ScriptableObject
{
    public int damage;

    public float maxHomingSpeed;
}
