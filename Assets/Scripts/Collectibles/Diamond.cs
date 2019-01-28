using UnityEngine;

public static class Diamond
{
    public const string DiamondType = "Diamond";

    public const string SmallDiamond = "SmallCoin";
    public const string MediumDiamond = "MediumCoin";
    public const string LargeDiamond = "LargeCoin";

    public static void PayDiamondCost(int cost)
    {
        PlayerPrefs.SetInt(DiamondType, GetCurrentDiamondAmount() - cost);
    }
    
    public static int GetCurrentDiamondAmount()
    {
        return PlayerPrefs.GetInt(DiamondType, 0);
    }

    public static void ResetDiamondAmount()
    {
        PlayerPrefs.SetInt(DiamondType, 0);
    }
}
