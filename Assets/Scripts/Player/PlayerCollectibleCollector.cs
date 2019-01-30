using UnityEngine;

public class PlayerCollectibleCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (SessionManager.Instance.IsGamePaused || !SessionManager.Instance.IsGameStarted)
        {
            return;
        }

        ICollectible collectible = other.GetComponent<ICollectible>();

        if (collectible != null && collectible.GetType() == Diamond.DiamondType)
        {
            SessionManager.Instance.IncreaseDiamondAmount(collectible.GetValue());
            ParticlesManager.Instance.IntantiateDiamondBlimp(transform.position, collectible.GetValue());

            Destroy(other.gameObject);
        }
    }
}
