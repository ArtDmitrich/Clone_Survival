using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] protected SpawnerSettings _spawnerSettings;

    [SerializeField] protected bool _poolPrewarming = true;
    [SerializeField] protected int _startPoolSize = 5;
    protected PoolManager _poolManager;

    protected void Awake()
    {
        _poolManager = gameObject.AddComponent<PoolManager>();
        _poolManager.Init(_spawnerSettings, _poolPrewarming, _startPoolSize);
    }
}
