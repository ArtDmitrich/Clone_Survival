using UnityEngine;

public class Bullet : MonoBehaviour
{
    private LayerMask _targetLayer;
    private float _damageValue;
    private bool _isPiercing;

    private PooledItem PooledItem { get { return _pooledItem = _pooledItem ?? GetComponent<PooledItem>(); } }
    private PooledItem _pooledItem;

    public void Init(LayerMask targetLayer, float damageValue, bool isPiercing)
    {
        _targetLayer = targetLayer;
        _damageValue = damageValue;
        _isPiercing = isPiercing;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_targetLayer & (1 << collision.gameObject.layer)) != 0 && collision.gameObject.TryGetComponent<ITakingDamage>(out var target))
        {
            target.TakeDamage(_damageValue);

            if (!_isPiercing)
            {
                Deactivate();
            }
        }
    }

    private void Deactivate()
    {
        if (gameObject.activeInHierarchy)
        {
            PooledItem.Release();
        }
    }
}
