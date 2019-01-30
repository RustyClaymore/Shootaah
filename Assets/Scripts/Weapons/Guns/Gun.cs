using UnityEngine;

public abstract class Gun : MonoBehaviour, IGun
{
    public bool IsShooting { get => isShooting; set => isShooting = value; }
    public GunDataSO gunData;
    public Transform[] barrels;

    protected bool isShooting;

    private float currentShootCooldown;

    public virtual void Start()
    {
        isShooting = false;
        currentShootCooldown = 0;
    }

    public virtual void Update()
    {
        currentShootCooldown -= Time.deltaTime;
    }

    public abstract int Shot();

    public bool IsReadyToShoot()
    {
        return currentShootCooldown <= 0f;
    }

    protected void ResetCooldown()
    {
        currentShootCooldown = 1f / gunData.fireRate;
    }
}