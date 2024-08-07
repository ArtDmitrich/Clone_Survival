using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class GameplayManager : MonoBehaviour
{
    public UnityAction<bool> GameplayEnded;
    public UnityAction<float> PlayerHealthChanged;

    public int[] PlayerHealthInfo
    {
        get { return new int[] { _playerHealthComponent.CurrentHealth, _playerHealthComponent.MaxHealth, _playerHealthComponent.HealthPerSec }; }
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

    private PlayerController _playerController;
    private HealthComponent _playerHealthComponent;

    [Inject]
    private void Construct(ResourceManager resourceManager, TimersManager timersManager, PickUpItemsManager pickUpItemsManager, EnemiesManager enemiesManager)
    {
        _resourceManager = resourceManager;
        _timersManager = timersManager;
        _pickUpItemsManager = pickUpItemsManager;
        _enemiesManager = enemiesManager;
    }
    public void StartGameplay()
    {
        _timersManager.SetTimer(_specialWaveColdown, StartNextSpecialWave);
        _timersManager.SetTimer(_timeToSpawnFirstEnemy, SpawnRandomEnemy);
        SpawnRandomPickUpItem();
    }

    private void Awake()
    {
        _playerController = _player.GetComponent<PlayerController>();
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

    private void OnEnable()
    {
        _playerController.CharacterDead += PlayerDead;
        _playerHealthComponent.HealthRationChanged += ChangeHealthValue;

        _enemiesManager.AllEnemiesDead += AllEnemyDead;

        _resourceManager.PlayerHealed += _playerHealthComponent.Heal;
    }

    private void OnDisable()
    {
        _playerController.CharacterDead -= PlayerDead;
        _playerHealthComponent.HealthRationChanged -= ChangeHealthValue;

        _enemiesManager.AllEnemiesDead -= AllEnemyDead;

        _resourceManager.PlayerHealed -= _playerHealthComponent.Heal;
    }
}
