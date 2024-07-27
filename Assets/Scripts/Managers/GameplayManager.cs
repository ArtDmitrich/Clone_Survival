using System.Collections;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private int _healItemColdown;
    [SerializeField] private int _goldItemColdown;
    [SerializeField] private int _manaItemColdown;

    [SerializeField] private int _smallManaChance = 60;
    [SerializeField] private int _mediumManaChance = 30;
    [SerializeField] private int _largeManaChance = 10;

    [SerializeField] private PlayerController _player;
    [SerializeField] private int _playerLives;
    [SerializeField] private WaveSettings _waves;

    private bool _wavesIsOver;
    private bool _chunksIsOver;

    private void Start()
    {
        StartCoroutine(StartLevel());        
        StartAddingPickUpItem();
    }

    private IEnumerator StartLevel()
    {
        _wavesIsOver = false;

        for (int i = 0; i < _waves.WavesDatas.Count; i++)
        {
            //StartCoroutine(StartWave(_waves.WavesDatas[i]));

            yield return StartCoroutine(StartWave(_waves.WavesDatas[i]));
        }
        
        _wavesIsOver = true;
    }

    private IEnumerator StartWave(WavesData waveData)
    {
        _chunksIsOver = false;

        foreach (var chunk in waveData.chunksOfWaves)
        {
            //StartCoroutine(StartChunk(chunk));

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

    private void StartAddingPickUpItem()
    {
        AddHeal();
        AddGold();
        AddMana();
    }

    private void AddHeal()
    {
        TimersManager.Instance.SetTimer(_healItemColdown, AddHeal);
        PickUpItemsManager.Instance.AddPickUpItem(PickUpItemType.Heal, _player.transform.position);
    }
    private void AddGold()
    {
        TimersManager.Instance.SetTimer(_goldItemColdown, AddGold);
        PickUpItemsManager.Instance.AddPickUpItem(PickUpItemType.Gold, _player.transform.position);
    }

    private void AddMana()
    {
        TimersManager.Instance.SetTimer(_manaItemColdown, AddMana);

        var chance = Random.Range(1, 101);

        PickUpItemType newItemType;

        if (chance <= _largeManaChance)
        {
            newItemType = PickUpItemType.LargeMana;
        }
        else if (chance <= _mediumManaChance) 
        {
            newItemType = PickUpItemType.MediumMana;
        }
        else
        {
            newItemType = PickUpItemType.SmallMana;
        }

        PickUpItemsManager.Instance.AddPickUpItem(newItemType, _player.transform.position);
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
        EnemiesManager.Instance.AllEnemiesDead += AllEnemyDead;
        _player.CharacterDead += PlayerDead;
    }

    private void OnDisable()
    {
        EnemiesManager.Instance.AllEnemiesDead -= AllEnemyDead;
        _player.CharacterDead -= PlayerDead;
    }
}
