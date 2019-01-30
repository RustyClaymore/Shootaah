using UnityEngine;

public class HomingMissileLauncher : PlayerJammableGun
{
    public Transform Target { get => target; set => target = value; }

    private Transform target;

    public override int Shot()
    {
        if (!IsReadyToShoot() || IsJammed() || isFixingJam)
        {
            return 0;
        }

        IsShooting = true;

        ResetCooldown();
        IncreaseJamRate();

        jamTime = Time.time;
        
        foreach (Transform barrel in barrels)
        {
            GameObject proj = Instantiate(gunData.projectilePrefab, barrel.position, Quaternion.identity) as GameObject;
            proj.transform.rotation = Quaternion.LookRotation(barrel.forward, Vector3.up);
            proj.GetComponent<Rigidbody>().AddForce(barrel.forward * gunData.speed, ForceMode.Impulse);
            Destroy(proj, 3);
        }

        return gunData.projectilePrefab.GetComponent<IProjectile>().GetDamage();
    }
}
