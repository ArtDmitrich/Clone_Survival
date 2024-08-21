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
    [SerializeField] private List<WeaponUpgradesHolder> _weaponUpgrades = new List<WeaponUpgradesHolder>();

    private Dictionary<string, Upgrade> _possibleUpgrades = new Dictionary<string, Upgrade>();

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

        var upgradeTitles = new List<string>();

        foreach (var upgrade in upgrades)
        {
            var title = upgrade.Title;
            _possibleUpgrades.Add(title, upgrade);
            upgradeTitles.Add(title);
        }
        
        _upgradeMenu.ActivateUpgradeButtons(upgradeTitles);
    }

    private void FinishUpgrade(string upgradeTitle)
    {
        var upgrade = _possibleUpgrades[upgradeTitle];

        upgrade.Activate(_playerController);

        UpgradingFinished?.Invoke();
        _possibleUpgrades.Clear();
    }

    private List<Upgrade> GetRandomUpgrades(int count, AttackingSystem attackingSystem)
    {
        UpdateWeaponUpgrades(attackingSystem);

        var allUpgrades = new List<Upgrade>();
        allUpgrades.AddRange(_charaterStatsUpgrades);

        foreach (var weaponUpgrade in _weaponUpgrades)
        {
            allUpgrades.Add(weaponUpgrade.CurrentUpgrade);
        }

        allUpgrades.Shuffle();

        var result = new List<Upgrade>(count);
        result.AddRange(allUpgrades.Take(count));

        return result;
    }

    private void UpdateWeaponUpgrades(AttackingSystem attackingSystem)
    {
        var listToDelete = new List<WeaponUpgradesHolder>();

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
