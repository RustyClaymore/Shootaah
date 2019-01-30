using UnityEngine;

public abstract class PlayerJammableGun : Gun
{
    public float CurrentJamRate { get => currentJamRate; }

    protected bool isFixingJam;
    protected float jamTime;
    
    private float currentJamCooldown;
    private float currentJamRate;

    public override void Start()
    {
        base.Start();
        isFixingJam = false;
        currentJamRate = 0;
        currentJamCooldown = 0;
        jamTime = float.PositiveInfinity;
    }

    public override void Update()
    {
        base.Update();
        if (!isShooting && (Time.time >= jamTime + gunData.timeBeforeFixStart))
        {
            currentJamCooldown -= Time.deltaTime;
            DecreaseJamRate();
        }
    }

    public bool IsJammed()
    {
        return (currentJamRate >= gunData.jamThreshold || isFixingJam);
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
