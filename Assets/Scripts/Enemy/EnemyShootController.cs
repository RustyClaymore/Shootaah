using UnityEngine;

public class EnemyShootController : MonoBehaviour
{
    private GameObject currentGun;

    public GameObject CurrentGun { set => currentGun = value; }

    public void Shot()
    {
        IGun enemyGun = currentGun.GetComponent<IGun>();
        int damage = enemyGun.Shot();
    }
}
