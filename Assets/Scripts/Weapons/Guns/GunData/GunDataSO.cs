using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "ScriptableObjects/GunData", order = 1)]
public class GunDataSO : ScriptableObject
{
    public GameObject projectilePrefab;

    public float speed;
    public float fireRate; // shots per second

    public float jamThreshold;
    public float jamRate;
    public float jamFixRate;
    public float timeBeforeFixStart;
    public float timeBetweenFixes;
}