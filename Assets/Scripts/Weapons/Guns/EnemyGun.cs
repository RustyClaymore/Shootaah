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
        
        int randomBarrel = Random.Range(0, barrels.Length);
        GameObject proj = Instantiate(gunData.projectilePrefab, barrels[randomBarrel].position, Quaternion.identity) as GameObject;
        proj.transform.rotation = Quaternion.LookRotation(barrels[randomBarrel].forward, Vector3.up);
        proj.GetComponent<Rigidbody>().AddForce(barrels[randomBarrel].forward * gunData.speed, ForceMode.Impulse);
        Destroy(proj, 5);

        return gunData.projectilePrefab.GetComponent<IProjectile>().GetDamage();
    }
}
