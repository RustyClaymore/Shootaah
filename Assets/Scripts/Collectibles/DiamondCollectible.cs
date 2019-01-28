using UnityEngine;

public class DiamondCollectible : MonoBehaviour, ICollectible
{
    public CollectibleDataSO CollectibleData { get => collectibleData; }

    [SerializeField]
    private CollectibleDataSO collectibleData;

    public string GetName()
    {
        return collectibleData.collectibleName;
    }
    
    public new string GetType()
    {
        return collectibleData.type;
    }

    public int GetValue()
    {
        return collectibleData.value;
    }
}
