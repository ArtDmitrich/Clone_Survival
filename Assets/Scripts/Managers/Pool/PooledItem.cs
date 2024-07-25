using UnityEngine;
using UnityEngine.Pool;

public class PooledItem: MonoBehaviour
{
    public IObjectPool<PooledItem> Pool;

    public void Release()
    {
        if (gameObject.activeInHierarchy)
        {
            Pool.Release(this);
        }
    }
}
