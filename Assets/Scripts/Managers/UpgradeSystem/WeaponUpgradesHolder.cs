using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponUpgradesHolder
{
    public WeaponType WeaponType;
    public WeaponUpgrade CurrentUpgrade;

    [SerializeField] private List<WeaponUpgrade> _weaponUpgrades;

    public bool TrySetCurrentUpgrade(int currentWeaponLevel)
    {
        if (currentWeaponLevel < 0)
        {
            currentWeaponLevel = 0;
        }

        var nextLevel = currentWeaponLevel + 1;

        for (int i = 0; i < _weaponUpgrades.Count; i++)
        {
            if (_weaponUpgrades[i].Level == nextLevel)
            {
                CurrentUpgrade = _weaponUpgrades[i];
                return true;
            }
        }

        return false;
    }

    public void ActivateUpgrade(PlayerController playerController)
    {
        CurrentUpgrade.Activate(playerController);
    }
}
