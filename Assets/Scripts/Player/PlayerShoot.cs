using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private GameObject currentGun;

    public GameObject CurrentGun { get => currentGun; set => currentGun = value; }
    
    void Update()
    {
        IGun playerGun = currentGun.GetComponent<IGun>();
        if (InputManager.Instance.Button1Pressed)
        {
            int damage = playerGun.Shot();
        } else
        {
            playerGun.IsShooting = false;
        }
    }
}
