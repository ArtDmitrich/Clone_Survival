using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnerSettings", order = 1)]
public class SpawnerSettings : ScriptableObject
{
    [SerializeField] private List<PooledItemData> _pooledItems;

    public PooledItem GetPooledItem(string itemName)
    {
        foreach (var item in _pooledItems)
        {
            if (item.Key == itemName)
            {
                return Instantiate(item.Value);
            }
        }
        return null;
    }
}
