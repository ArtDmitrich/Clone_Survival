using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class UpgradeButtonManager : ItemManager
{
    private List<PooledItem> _items = new List<PooledItem>();
    public UpgradeButton GetUpgradeButton(string upgradeButtonName)
    {
        var upgradeButton = _poolManager.GetPooledItem<UpgradeButton>(upgradeButtonName);

        if (upgradeButton != null && upgradeButton.TryGetComponent<PooledItem>(out var pooledItem))
        {
            upgradeButton.transform.SetParent(transform);
            _items.Add(pooledItem);
        }

        return _poolManager.GetPooledItem<UpgradeButton>(upgradeButtonName);
    }

    public void ClearUpgradeList()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].Release();
        }
    }
}
