using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableEnemy : Character
{
    private PooledItem PooledItem { get { return _pooledItem = _pooledItem ?? GetComponent<PooledItem>(); } }
    private PooledItem _pooledItem;

    protected override void Death()
    {
        base.Death();
        PooledItem.Release();
    }
}
