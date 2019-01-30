using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChooserPlaceHolder : MonoBehaviour
{
    public void SelectMissileLauncher()
    {
        PlayerPrefs.SetString("CurrentWeapon", "MissileLauncher");
    }

    public void SelectHomingMissileLauncher()
    {
        PlayerPrefs.SetString("CurrentWeapon", "HomingMissileLauncher");
    }
}
