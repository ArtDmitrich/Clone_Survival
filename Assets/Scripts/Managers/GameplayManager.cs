using System.Collections;
using UnityEngine;
using UnityEngine.Windows;
using Zenject;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private int _pickUpItemColdown;

    [SerializeField] private Transform _player;
    [SerializeField] private int _playerLives;

    [SerializeField] private float _spawnEnemyColdown;
    [SerializeField] private float _specialWaveColdown;

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

    private void Awake()
    {
        _playerController = _player.GetComponent<PlayerController>();
        _playerHealthComponent = _player.GetComponent<HealthComponent>();
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        _timersManager.SetTimer(_specialWaveColdown, StartNextSpecialWave);
        SpawnRandomPickUpItem();
        SpawnRandomEnemy();
    }

    private void SpawnRandomEnemy()
    {
        _timersManager.SetTimer(_spawnEnemyColdown, SpawnRandomEnemy);
        _enemiesManager.SpawnRandomEnemy(_player);
    }

    private void StartNextSpecialWave()
    {
        _timersManager.SetTimer(_specialWaveColdown, StartNextSpecialWave);
        _enemiesManager.StartNextSpecialWave(_player);
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
    }

    private void PlayerLose()
    {
        Debug.LogWarning("GAME OVER.");
    }

    private void OnEnable()
    {
        _playerController.CharacterDead += PlayerDead;

        _enemiesManager.AllEnemiesDead += AllEnemyDead;

        _resourceManager.PlayerHealed += _playerHealthComponent.Heal;
    }

    private void OnDisable()
    {
        _playerController.CharacterDead -= PlayerDead;

        _enemiesManager.AllEnemiesDead -= AllEnemyDead;

        _resourceManager.PlayerHealed -= _playerHealthComponent.Heal;
    }
}
