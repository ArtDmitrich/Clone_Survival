using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;
using Zenject;

public class GameplayManager : MonoBehaviour
{
    public UnityAction<bool> GameplayEnded;
    public UnityAction<float> PlayerHealthChanged;
    public UnityAction<List<Upgrade>> PlayeraLevelUpped;

    public Vector3 PlayerHealthInfo
    {
        get { return new Vector3(_playerStats.CurrentHealth, _playerStats.MaxHealth, _playerStats.HealthPerSec); }
    }

    [SerializeField] private int _pickUpItemColdown;

    [SerializeField] private Transform _player;
    [SerializeField] private int _playerLives;

    [SerializeField] private float _timeToSpawnFirstEnemy;
    [SerializeField] private float _spawnEnemyColdown;
    [SerializeField] private float _minSpawnEnemyColdown;
    [SerializeField] private float _specialWaveColdown;

    [SerializeField] private float _decreaseEnemyColdownValue;
    [SerializeField] private int _enemySpawnCount;

    private ResourceManager _resourceManager;
    private TimersManager _timersManager;
    private PickUpItemsManager _pickUpItemsManager;
    private EnemiesManager _enemiesManager;
    private UpgradeSystem _upgradeSystem;

    private PlayerController _playerController;
    private HealthComponent _playerHealthComponent;
    private CharacterStats _playerStats;

    [Inject]
    private void Construct(ResourceManager resourceManager, TimersManager timersManager, PickUpItemsManager pickUpItemsManager, EnemiesManager enemiesManager, UpgradeSystem upgradeSystem)
    {
        _resourceManager = resourceManager;
        _timersManager = timersManager;
        _pickUpItemsManager = pickUpItemsManager;
        _enemiesManager = enemiesManager;
        _upgradeSystem = upgradeSystem;
    }
    public void StartGameplay()
    {
        _timersManager.SetTimer(_specialWaveColdown, StartNextSpecialWave);
        _timersManager.SetTimer(_timeToSpawnFirstEnemy, SpawnRandomEnemy);
        SpawnRandomPickUpItem();
    }
    public void UpgradePlayer(Upgrade upgrade)
    {
        upgrade.Activate(_playerController);
    }

    private void Awake()
    {
        _playerController = _player.GetComponent<PlayerController>();
        _playerStats = _playerController.CharacterStats;
        _playerHealthComponent = _playerController.HealthComponent;
    }

    private void SpawnRandomEnemy()
    {
        _timersManager.SetTimer(_spawnEnemyColdown, SpawnRandomEnemy);
        _enemiesManager.SpawnRandomEnemies(_player, _enemySpawnCount);
    }

    private void StartNextSpecialWave()
    {
        _timersManager.SetTimer(_specialWaveColdown, StartNextSpecialWave);
        _enemiesManager.StartNextSpecialWave(_player);

        //some logic for adding enemy spawn count
        _enemySpawnCount += _enemySpawnCount;
        _spawnEnemyColdown -= _decreaseEnemyColdownValue;

        if (_spawnEnemyColdown <= _minSpawnEnemyColdown)
        {
            _spawnEnemyColdown = _minSpawnEnemyColdown;
        }
    }

    private void SpawnRandomPickUpItem()
    {
        _timersManager.SetTimer(_pickUpItemColdown, SpawnRandomPickUpItem);
        _pickUpItemsManager.SpawnRandomPickUpItem(_player.position);
    }

    private void AllEnemyDead()
    {
        PlayerWin();
    }

    private void PlayerDead(Character player)
    {
        _playerLives--;

        if (_playerLives <= 0)
        {
            PlayerLose();
        }
    }

    private void PlayerWin()
    {
        Debug.LogWarning("PlayerWin!!!");
        GameplayEnded?.Invoke(true);
    }

    private void PlayerLose()
    {
        Debug.LogWarning("GAME OVER.");
        GameplayEnded?.Invoke(false);
    }

    private void ChangeHealthValue(float value)
    {
        PlayerHealthChanged?.Invoke(value);
    }

    private void PlayerLevelUp()
    {
        //TODO: ����������� ����� ������������ ���� ��������� ���������
        var possibleUpgrades = _upgradeSystem.GetRandomUpgrades(3, _playerController.AttackingSystem);
        PlayeraLevelUpped?.Invoke(possibleUpgrades);
    }    

    private void OnEnable()
    {
        _playerController.CharacterDead += PlayerDead;
        _playerHealthComponent.HealthRationChanged += ChangeHealthValue;

        _enemiesManager.AllEnemiesDead += AllEnemyDead;

        _resourceManager.PlayerHealed += _playerHealthComponent.Heal;
        _resourceManager.PlayersLevelUpped += PlayerLevelUp;
    }

    private void OnDisable()
    {
        _playerController.CharacterDead -= PlayerDead;
        _playerHealthComponent.HealthRationChanged -= ChangeHealthValue;

        _enemiesManager.AllEnemiesDead -= AllEnemyDead;

        _resourceManager.PlayerHealed -= _playerHealthComponent.Heal;
        _resourceManager.PlayersLevelUpped -= PlayerLevelUp;
    }
}
