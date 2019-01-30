using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject currentGun;

    public GameObject CurrentGun { get => currentGun; set => currentGun = value; }

    void Update()
    {
        if (SessionManager.Instance.IsGamePaused || !SessionManager.Instance.IsGameStarted)
        {
            return;
        }

        IGun playerGun = currentGun.GetComponent<IGun>();
        if (InputManager.Instance.AttackButtonPressed)
        {
            int damage = playerGun.Shot();
        }
        else
        {
            playerGun.IsShooting = false;
        }
    }
}
