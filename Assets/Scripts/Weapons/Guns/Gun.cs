using UnityEngine;

public abstract class Gun : MonoBehaviour, IGun
{
    public bool IsShooting { get => isShooting; set => isShooting = value; }

    [SerializeField]
    protected GunDataSO gunData;
    [SerializeField]
    protected Transform barrel;

    protected bool isFixingJam;
    protected float jamTime;
    
    private float currentShootCooldown;
    private float currentJamCooldown;
    [SerializeField]
    private float currentJamRate;
    private bool isShooting;

    void Start()
    {
        isShooting = false;
        isFixingJam = false;
        currentJamRate = 0;
        currentJamCooldown = 0;
        jamTime = float.PositiveInfinity;
    }

    void Update()
    {
        currentShootCooldown -= Time.deltaTime;
        if (!isShooting && (Time.time >= jamTime + gunData.timeBeforeFixStart))
        {
            currentJamCooldown -= Time.deltaTime;
            DecreaseJamRate();
        }
    }

    public abstract int Shot();

    public bool IsReadyToShoot()
    {
        return currentShootCooldown < 0f;
    }
    
    public bool IsJammed()
    {
        return (currentJamRate >= gunData.jamThreshold || isFixingJam);
    }

    protected void ResetCooldown()
    {
        currentShootCooldown = 1f / gunData.fireRate;
    }

    protected void IncreaseJamRate()
    {
        currentJamRate = Mathf.Min(currentJamRate + gunData.jamRate, gunData.jamThreshold);
    }

    protected void DecreaseJamRate()
    {
        if (currentJamCooldown > 0)
        {
            isFixingJam = false;
            return;
        }

        currentJamRate = Mathf.Max(currentJamRate - gunData.jamFixRate, 0);
        
        currentJamCooldown = gunData.timeBetweenFixes;
        isFixingJam = currentJamRate > 0;
    }
}
