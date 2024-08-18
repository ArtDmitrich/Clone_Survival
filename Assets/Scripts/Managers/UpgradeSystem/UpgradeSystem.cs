using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class UpgradeSystem : MonoBehaviour
{
    public UnityAction UpgradingStarted;
    public UnityAction UpgradingFinished;

    [SerializeField] private List<CharacterStatsUpgrade> _charaterStatsUpgrades = new List<CharacterStatsUpgrade>();
    [SerializeField] private List<WeaponUpgrade> _weaponUpgrades = new List<WeaponUpgrade>();

    private UpgradeMenu _upgradeMenu;
    private PlayerController _playerController;

    [Inject]
    private void Construct(UpgradeMenu upgradeMenu)
    {
        _upgradeMenu = upgradeMenu;
    }

    public void StartUpgrade(int possibleUpgradesCount, PlayerController playerController)
    {
        UpgradingStarted?.Invoke();

        _playerController = playerController;

        var upgrades = GetRandomUpgrades(possibleUpgradesCount, _playerController.AttackingSystem);
        _upgradeMenu.ActivateUpgradeButtons(upgrades);
    }

    private void FinishUpgrade(Upgrade upgrade)
    {
        upgrade.Activate(_playerController);

        UpgradingFinished?.Invoke();
    }

    private List<Upgrade> GetRandomUpgrades(int count, AttackingSystem attackingSystem)
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

    private void OnEnable()
    {
        _upgradeMenu.UpgradeSelected += FinishUpgrade;
    }

    private void OnDisable()
    {
        _upgradeMenu.UpgradeSelected -= FinishUpgrade;
    }
}
