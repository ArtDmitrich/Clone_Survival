using UnityEngine;
using UnityEngine.Events;

public class PickUpItem : MonoBehaviour
{
    public UnityAction<PickUpItem> ItemPickedUp;    
    public PickUpItemType ItemType;
    public float Value;

    [SerializeField] private LayerMask _targetLayer;

    private PooledItem PooledItem { get { return _pooledItem = _pooledItem ?? GetComponent<PooledItem>(); } }
    private PooledItem _pooledItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((_targetLayer & (1 << collision.gameObject.layer)) != 0)
        {
            ItemPickedUp?.Invoke(this);
            Deactivate();
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
