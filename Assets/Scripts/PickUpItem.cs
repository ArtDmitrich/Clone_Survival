using UnityEngine;
using UnityEngine.Events;

public class PickUpItem : MonoBehaviour
{
    public UnityAction<PickUpItem> ItemPickedUp;    
    public PickUpItemType ItemType;
    public float Value;

    private PooledItem PooledItem { get { return _pooledItem = _pooledItem ?? GetComponent<PooledItem>(); } }
    private PooledItem _pooledItem;

    private void OnTriggerEnter2D(Collider2D collision)
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
