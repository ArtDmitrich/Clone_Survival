using UnityEngine;
using UnityEngine.Events;

public class PickUpItem : MonoBehaviour
{
    public UnityAction<PickUpItem> ItemPickedUp;
    public AttributesSet Attributes;

    private PooledItem PooledItem { get { return _pooledItem = _pooledItem ?? GetComponent<PooledItem>(); } }
    private PooledItem _pooledItem;

    private void OnTriggerEnter2D()
    {
        ItemPickedUp?.Invoke(this);
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
