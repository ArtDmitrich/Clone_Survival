using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Upgrades/Weapon", order = 1)]

public class WeaponUpgrade : Upgrade
{
    public WeaponType WeaponType;
    public WeaponUpgradeInfo CurrentUpgrade;

    [SerializeField] private List<WeaponUpgradeInfo> _upgradesInfo;

    public bool TrySetCurrentUpgrade(int currentWeaponLevel)
    {
        if (currentWeaponLevel < 0)
        {
            currentWeaponLevel = 0;
        }

        var nextLevel = currentWeaponLevel + 1;

        for (int i = 0; i < _upgradesInfo.Count; i++)
        {
            if (_upgradesInfo[i].Level == nextLevel)
            {
                CurrentUpgrade = _upgradesInfo[i];
                Title = CurrentUpgrade.Title;
                return true;
            }
        }

        return false;
    }

    public override void Activate(PlayerController player)
    {
        var weapon = Object.Instantiate(CurrentUpgrade.Prefab);
        player.AttackingSystem.AddNewWeapon(weapon);
    }
}
