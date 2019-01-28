using UnityEngine;

[CreateAssetMenu(fileName = "CollectibleData", menuName = "ScriptableObjects/CollectibleData", order = 1)]
public class CollectibleDataSO : ScriptableObject
{
    public string collectibleName;
    public string type;
    public int value;
}
