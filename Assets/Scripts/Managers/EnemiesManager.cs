using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesManager : ItemManager<EnemiesManager>
{
    public UnityAction AllEnemiesDead;

    [SerializeField] private List<Transform> _movableEnemySpots = new List<Transform>();
    [SerializeField] private List<Transform> _immovableEnemySpots = new List<Transform>();

    private List<Character> _enemies = new List<Character>();

    public void AddEnemy(EnemyType enemyType, Transform player)
    {
        var item = GetEnemy(enemyType.ToString());

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

        enemy.gameObject.SetActive(false);
    }
}
