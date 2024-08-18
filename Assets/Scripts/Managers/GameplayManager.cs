using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class GameplayManager : MonoBehaviour
{
    public UnityAction<bool> GameplayEnded;

    [SerializeField] private int _playerLives;
    [SerializeField] private float _timeToSpawnFirstEnemy;
    [SerializeField] private int _possibleUpgradesCount = 3;

    private float _pickUpItemColdown;

    private float _spawnEnemyColdown;
    private float _minSpawnEnemyColdown;
    private float _specialWaveColdown;
    private float _decreaseEnemyColdownValue;
    private int _enemySpawnCount;
    private bool _gameModeIsEndless;

    private ResourceManager _resourceManager;
    private TimersManager _timersManager;
    private PickUpItemsManager _pickUpItemsManager;
    private EnemiesManager _enemiesManager;
    private UpgradeSystem _upgradeSystem;

    private PlayerController _playerController;

    [Inject]
    private void Construct(PlayerController playerController, ResourceManager resourceManager, TimersManager timersManager, PickUpItemsManager pickUpItemsManager, EnemiesManager enemiesManager,
        UpgradeSystem upgradeSystem)
    {
        _playerController = playerController;

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

    public void SetGameSettings(bool gameModeIsEndless, WaveSettings waveSettings, EnemyUpgradeSettings enemyUpgradeSettings)
    {
        _gameModeIsEndless = gameModeIsEndless;

        _spawnEnemyColdown = waveSettings.StartSpawnEnemyColdown;
        _minSpawnEnemyColdown = waveSettings.MinSpawnEnemyColdown;
        _specialWaveColdown = waveSettings.SpecialWaveColdown;
        _decreaseEnemyColdownValue = waveSettings.DecreaseEnemyColdownValue;
        _enemySpawnCount = waveSettings.StartEnemySpawnCount;
        _pickUpItemColdown = waveSettings.PickUpItemColdown;

        _enemiesManager.SetEnemmiesManagerSettings(waveSettings, enemyUpgradeSettings);
    }

    private void SpawnRandomEnemy()
    {
        _timersManager.SetTimer(_spawnEnemyColdown, SpawnRandomEnemy);
        _enemiesManager.SpawnRandomEnemies(_playerController.transform, _enemySpawnCount);
    }

    private void StartNextSpecialWave()
    {
        _timersManager.SetTimer(_specialWaveColdown, StartNextSpecialWave);
        _enemiesManager.StartNextSpecialWave(_playerController.transform);

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
        _pickUpItemsManager.SpawnRandomPickUpItem(_playerController.transform.position);
    }

    private void StopAllSpawners()
    {
        if (!_gameModeIsEndless)
        {
            _timersManager.RemoveAllTimers();
        }
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
        else
        {
            //TODO: player respawn logic
        }
    }

    private void PlayerWin()
    {
        GameplayEnded?.Invoke(true);
    }

    private void PlayerLose()
    {
        GameplayEnded?.Invoke(false);
    }

    private void PlayerLevelUp()
    {
        _upgradeSystem.StartUpgrade(_possibleUpgradesCount, _playerController);
    }

    private void OnEnable()
    {
        _playerController.CharacterDead += PlayerDead;

        _enemiesManager.AllEnemiesDead += AllEnemyDead;
        _enemiesManager.AllSpecialWavesIsOvered += StopAllSpawners;

        _resourceManager.PlayersLevelUpped += PlayerLevelUp;
    }

    private void OnDisable()
    {
        _playerController.CharacterDead -= PlayerDead;

        _enemiesManager.AllEnemiesDead -= AllEnemyDead;
        _enemiesManager.AllSpecialWavesIsOvered -= StopAllSpawners;

        _resourceManager.PlayersLevelUpped -= PlayerLevelUp;
    }
}
