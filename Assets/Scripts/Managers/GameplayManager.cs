using System.Collections;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private int _pickUpItemColdown;

    [SerializeField] private PlayerController _player;
    [SerializeField] private int _playerLives;
    [SerializeField] private WaveSettings _waves;

    private bool _wavesIsOver;
    private bool _chunksIsOver;

    private void Start()
    {
        StartCoroutine(StartLevel());
        AddingPickUpItem();
    }

    private IEnumerator StartLevel()
    {
        _wavesIsOver = false;

        for (int i = 0; i < _waves.WavesDatas.Count; i++)
        {
            yield return StartCoroutine(StartWave(_waves.WavesDatas[i]));
        }
        
        _wavesIsOver = true;
    }

    private IEnumerator StartWave(WavesData waveData)
    {
        _chunksIsOver = false;

        foreach (var chunk in waveData.chunksOfWaves)
        {
            yield return StartCoroutine(StartChunk(chunk));
        }

        _chunksIsOver = true;
    }
    private IEnumerator StartChunk(ChunkWavesData chunk)
    {
        for (var i = 0; i < chunk.Count; i++)
        {
            EnemiesManager.Instance.AddEnemy(chunk.EnemyType, _player.transform);

            yield return new WaitForSeconds(chunk.EnemySpawnColdown);
        }
    }

    private void AddingPickUpItem()
    {
        TimersManager.Instance.SetTimer(_pickUpItemColdown, AddingPickUpItem);
        PickUpItemsManager.Instance.AddPickUpItem(_player.transform.position);
    }

    private void AllEnemyDead()
    {
        if (_chunksIsOver && _wavesIsOver)
        {
            PlayerWin();
        }
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
        if (EnemiesManager.Instance != null)
        {
            EnemiesManager.Instance.AllEnemiesDead += AllEnemyDead;
        }
        _player.CharacterDead += PlayerDead;
    }

    private void OnDisable()
    {
        if (EnemiesManager.Instance != null)
        {
            EnemiesManager.Instance.AllEnemiesDead -= AllEnemyDead;
        }

        _player.CharacterDead -= PlayerDead;
    }
}
