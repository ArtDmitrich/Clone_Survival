using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager: MonoBehaviour
{
    protected List<PoolByName> _pools;
    protected bool _poolPrewarming;
    protected int _startPoolSize;

    private SpawnerSettings _spawnerSettings;

    public void Init(SpawnerSettings spawnerSettings, bool poolPrewarming, int startPoolSize)
    {
        _pools = new List<PoolByName>();
        _spawnerSettings = spawnerSettings;
        _poolPrewarming = poolPrewarming;
        _startPoolSize = startPoolSize;
    }

    public T GetPooledItem<T>(string pooledItemName) where T : MonoBehaviour
    {
        var pool = GetPool(pooledItemName);

        if (pool == null)
        {
            AddPool(pooledItemName, out pool);
        }

        try
        {
            return pool.Pool.Get().GetComponent<T>();
        }
        catch (ArgumentNullException ex)
        {
            Debug.LogWarning(ex.Message);
            return null;
        }
    }

    private PoolByName GetPool(string pooledItemName)
    {
        foreach (var pool in _pools)
        {
            if (pool.PooledItemName == pooledItemName)
            {
                return pool;
            }
        }

        return null;
    }

    private void AddPool(string pooledItemName, out PoolByName newPool)
    {
        var newObj = new GameObject();
        newObj.transform.SetParent(transform);
        newObj.name = pooledItemName + "Pool";

        newPool = new PoolByName(pooledItemName, _spawnerSettings, newObj.transform);

        _pools.Add(newPool);

        if (_poolPrewarming)
        {
            Prewarm(newPool);
        }
    }

    private void Prewarm(PoolByName pool)
    {
        var startingItems = new List<PooledItem>();

        for (int i = 0; i < _startPoolSize; i++)
        {
            try
            {
                var item = pool.Pool.Get();
                startingItems.Add(item);
            }
            catch (ArgumentNullException ex)
            {
                Debug.LogWarning(ex.Message);
                return;
            }
        }

        foreach (var item in startingItems)
        {
            item.Release();
        }
    }
}
