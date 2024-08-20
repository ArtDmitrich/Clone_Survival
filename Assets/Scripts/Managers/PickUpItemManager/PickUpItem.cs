using UnityEngine;
using UnityEngine.Events;

public class PickUpItem : MonoBehaviour
{
    public UnityAction<PickUpItem, bool> ItemPickedUp;
    public AttributesSet Attributes;

    [SerializeField] private LayerMask _targetLayer;

    private PooledItem PooledItem { get { return _pooledItem = _pooledItem ?? GetComponent<PooledItem>(); } }
    private PooledItem _pooledItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var isPlayerPickedUp = false;

        if ((_targetLayer & (1 << collision.gameObject.layer)) != 0)
        {
            isPlayerPickedUp = true;
        }

        ItemPickedUp?.Invoke(this, isPlayerPickedUp);
        Deactivate();
    }

    private void Deactivate()
    {
        if (gameObject.activeInHierarchy)
        {
            PooledItem.Release();
        }
    }
}
