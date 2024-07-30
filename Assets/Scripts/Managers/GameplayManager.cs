using System.Collections;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private int _pickUpItemColdown;

    [SerializeField] private Transform _player;
    [SerializeField] private int _playerLives;

    [SerializeField] private float _spawnEnemyColdown;
    [SerializeField] private float _specialWaveColdown;

    private PlayerController _playerController;
    private HealthComponent _playerHealthComponent;
    
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
        TimersManager.Instance.SetTimer(_specialWaveColdown, StartNextSpecialWave);
        SpawnRandomPickUpItem();
        SpawnRandomEnemy();
    }

    private void SpawnRandomEnemy()
    {
        TimersManager.Instance.SetTimer(_spawnEnemyColdown, SpawnRandomEnemy);
        EnemiesManager.Instance.SpawnRandomEnemy(_player);
    }

    private void StartNextSpecialWave()
    {
        TimersManager.Instance.SetTimer(_specialWaveColdown, StartNextSpecialWave);
        EnemiesManager.Instance.StartNextSpecialWave(_player);
    }

    private void SpawnRandomPickUpItem()
    {
        TimersManager.Instance.SetTimer(_pickUpItemColdown, SpawnRandomPickUpItem);
        PickUpItemsManager.Instance.SpawnRandomPickUpItem(_player.position);
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

        if (EnemiesManager.Instance != null)
        {
            EnemiesManager.Instance.AllEnemiesDead += AllEnemyDead;
        }

        if (ResourceManager.Instance != null)
        {
            ResourceManager.Instance.PlayerHealed += _playerHealthComponent.Heal;
        }
    }

    private void OnDisable()
    {
        _playerController.CharacterDead -= PlayerDead;

        if (EnemiesManager.Instance != null)
        {
            EnemiesManager.Instance.AllEnemiesDead -= AllEnemyDead;
        }


        if (ResourceManager.Instance != null)
        {
            ResourceManager.Instance.PlayerHealed -= _playerHealthComponent.Heal;
        }
    }
}
