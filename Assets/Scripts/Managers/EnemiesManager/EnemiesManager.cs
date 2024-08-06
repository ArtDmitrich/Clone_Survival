using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class EnemiesManager : ItemManager
{
    public UnityAction AllEnemiesDead;

    [Tooltip("The sum of all chances must not exceed 100.")]
    [SerializeField] private ItemsWithChances _enemiesSpawnChances;

    [SerializeField] private WaveSettings _specialWaves;

    [SerializeField] private List<Transform> _movableEnemySpots = new List<Transform>();
    [SerializeField] private List<Transform> _immovableEnemySpots = new List<Transform>();

    private readonly List<Character> _enemies = new List<Character>();
    private int _currentWaveNumber = 0;

    private ResourceManager _resourceManager;

    [Inject]
    private void Construct(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;
    }

    public void SpawnRandomEnemies(Transform player, int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            AddEnemy(_enemiesSpawnChances.GetRandomItemName(), player);
        }
    }

    public void StartNextSpecialWave(Transform player)
    {           
        StartCoroutine(StartWave(_specialWaves.WavesDatas[_currentWaveNumber], player));

        if (_currentWaveNumber < _specialWaves.WavesDatas.Count - 1)
        {
            _currentWaveNumber++;
        }
    }

    private IEnumerator StartWave(WaveData waveData, Transform player)
    {
        foreach (var chunk in waveData.chunksOfWaves)
        {
            yield return StartCoroutine(StartChunk(chunk, player));
        }
    }
    private IEnumerator StartChunk(ChunkWaveData chunk, Transform player)
    {
        for (var i = 0; i < chunk.Count; i++)
        {
            AddEnemy(chunk.EnemyType.ToString(), player);

            yield return new WaitForSeconds(chunk.EnemySpawnColdown);
        }
    }

    private void AddEnemy(string enemyName, Transform player)
    {
        var item = GetEnemy(enemyName);

        if (item != null)
        {
            if (item.TryGetComponent<MovableEnemy>(out var movableEnemy))
            {
                //some logic to variable spot for spawn enemy
                var newPos = _movableEnemySpots[Random.Range(0, _movableEnemySpots.Count)].position;
                movableEnemy.transform.position = newPos;

                movableEnemy.Target = player;

                movableEnemy.CharacterDead += EnemyKilledByPlayer;
                _enemies.Add(movableEnemy);
            }
            else if (item.TryGetComponent<ImmovableEnemy>(out var immovableEnemy))
            {
                var newPos = _immovableEnemySpots[Random.Range(0, _immovableEnemySpots.Count)].position;
                immovableEnemy.transform.position = newPos;

                immovableEnemy.CharacterDead += EnemyKilledByPlayer;
                _enemies.Add(immovableEnemy);
            }
        }
    }

    public void RemoveEnemy(Character enemy)
    {
        if (_enemies.Contains(enemy))
        {
            enemy.CharacterDead -= EnemyKilledByPlayer;

            _enemies.Remove(enemy);

            if (_enemies.Count == 0)
            {
                AllEnemiesDead?.Invoke();
            }
        }
    }

    private Character GetEnemy(string enemyName)
    {
        return _poolManager.GetPooledItem<Character>(enemyName);
    }

    private void EnemyKilledByPlayer(Character enemy)
    {
        if (enemy == null)
        {
            return;
        }

        RemoveEnemy(enemy);

        _resourceManager.PlayerKilledEnemy();
    }
}
