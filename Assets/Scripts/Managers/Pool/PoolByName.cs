using System;
using UnityEngine;
using UnityEngine.Pool;

public class PoolByName
{
    public string PooledItemName;

    public IObjectPool<PooledItem> Pool
    {
        get
        {
            if (m_Pool == null)
            {
                m_Pool = new ObjectPool<PooledItem>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, _collectionChecks, _defaultCapacity, _maxPoolSize);
            }

            return m_Pool;
        }
    }

    private IObjectPool<PooledItem> m_Pool;

    private bool _collectionChecks;
    private int _defaultCapacity;
    private int _maxPoolSize;

    private Transform _parentForItems;
    private SpawnerSettings _spawnerSettings;

    public PoolByName(string pooledItemName, SpawnerSettings spawnerSettings, Transform parentForItem, bool collectionChecks = true, int defaultCapacity = 10, int maxPoolSize = 50)
    {
        PooledItemName = pooledItemName;
        _spawnerSettings = spawnerSettings;
        _parentForItems = parentForItem;
        _collectionChecks = collectionChecks;
        _defaultCapacity = defaultCapacity;
        _maxPoolSize = maxPoolSize;
    }
    
    public void Init(SpawnerSettings spawnerSettings)
    {
        _spawnerSettings = spawnerSettings;
    }

    private PooledItem CreatePooledItem()
    {
        if (_spawnerSettings == null)
        {
            throw new ArgumentNullException(PooledItemName + "Pool", "The spawner is null. You need to call the method Init(SpawnerSettings spawnerSettings) and set the spawner");
        }

        var item = _spawnerSettings.GetPooledItem(PooledItemName);

        if (item == null)
        {
            throw new ArgumentNullException(_spawnerSettings.name, $"The spawner does not contain a link to the prefab with name: {PooledItemName}");
        }

        item.transform.SetParent(_parentForItems);
        item.Pool = Pool;

        return item;
    }

    private void OnReturnedToPool(PooledItem item)
    {
        item.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(PooledItem item)
    {
        item.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(PooledItem item)
    {
        GameObject.Destroy(item.gameObject);
    }
}
