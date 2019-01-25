using UnityEngine;

public class EnemyGun : Gun
{
    public override int Shot()
    {
        if (!IsReadyToShoot())
        {
            return 0;
        }

        IsShooting = true;

        ResetCooldown();

        GameObject proj = Instantiate(gunData.projectilePrefab, barrel.position, Quaternion.identity) as GameObject;
        proj.GetComponent<Rigidbody>().AddForce(barrel.forward * gunData.speed, ForceMode.Impulse);
        Destroy(proj, 5);

        return gunData.projectilePrefab.GetComponent<IProjectile>().GetDamage();
    }
}
