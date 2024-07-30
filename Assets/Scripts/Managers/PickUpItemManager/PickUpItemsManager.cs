using System.Collections.Generic;
using UnityEngine;

public class PickUpItemsManager : ItemManager<PickUpItemsManager>
{
    [Tooltip("The sum of all chances must not exceed 100.")]
    [SerializeField] private ItemsWithChances _pickUpItemsChances;

    [SerializeField] private float _maxRangeSpot;

    private readonly List<PickUpItem> _pickUpItems = new List<PickUpItem>();

    public void SpawnRandomPickUpItem(Vector2 centerPos)
    {
        AddPickUpItem(_pickUpItemsChances.GetRandomItemName(), centerPos);
    }

    private void AddPickUpItem(string pickUpItemName, Vector2 centerPos)
    {
        var item = GetPickUpItem(pickUpItemName);
        
        if (item.gameObject.TryGetComponent<PickUpItem>(out var pickUpItem))
        {
            var newPos = GetRadomPosition(centerPos);
            pickUpItem.transform.position = newPos;

            _pickUpItems.Add(pickUpItem);

            pickUpItem.ItemPickedUp += ItemPickedUp;
        }
    }

    private PickUpItem GetPickUpItem(string pickUpItemName)
    {
        return _poolManager.GetPooledItem<PickUpItem>(pickUpItemName);
    }

    private Vector2 GetRadomPosition(Vector2 centerPos)
    {
        var offsetX = Random.Range(-_maxRangeSpot, _maxRangeSpot);
        var offsetY = Random.Range(-_maxRangeSpot, _maxRangeSpot);

        return new Vector2(centerPos.x + offsetX, centerPos.y + offsetY);
    }

    private void ItemPickedUp(PickUpItem pickUpItem)
    {
        pickUpItem.ItemPickedUp -= ItemPickedUp;
        _pickUpItems.Remove(pickUpItem);
        ResourceManager.Instance.PlayerPickUpItem(pickUpItem);
    }
}
