using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private List<CharacterStatsUpgrade> _charaterStatsUpgrades = new List<CharacterStatsUpgrade>();
    [SerializeField] private List<WeaponUpgrade> _weaponUpgrades = new List<WeaponUpgrade>();

    public List<Upgrade> GetRandomUpgrades(int count, AttackingSystem attackingSystem)
    {
        UpdateWeaponUpgrades(attackingSystem);

        var allUpgrades = new List<Upgrade>();
        allUpgrades.AddRange(_charaterStatsUpgrades);
        allUpgrades.AddRange(_weaponUpgrades);
        allUpgrades.Shuffle();

        var result = new List<Upgrade>(count);
        result.AddRange(allUpgrades.Take(count));

        return result;
    }

    private void UpdateWeaponUpgrades(AttackingSystem attackingSystem)
    {
        var listToDelete = new List<WeaponUpgrade>();

        for (int i = 0; i < _weaponUpgrades.Count; i++)
        {
            var upgrade = _weaponUpgrades[i];
            var previousUpgradeLevel = attackingSystem.GetWeaponLevel(upgrade.WeaponType);
            var upgradeIsSet = upgrade.TrySetCurrentUpgrade(previousUpgradeLevel);

            if(!upgradeIsSet)
            {
                listToDelete.Add(upgrade);
            }
        }

        for (int i = 0; i < listToDelete.Count; i++)
        {
            _weaponUpgrades.Remove(listToDelete[i]);
        }
    }
}
