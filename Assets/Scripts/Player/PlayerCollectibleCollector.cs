using UnityEngine;

public class PlayerCollectibleCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ICollectible collectible = other.GetComponent<ICollectible>();

        if (collectible != null && collectible.GetType() == Coin.CoinType)
        {
            SessionManager.Instance.IncreaseCoinAmount(collectible.GetValue());
            Destroy(other.gameObject);
        }
    }
}
