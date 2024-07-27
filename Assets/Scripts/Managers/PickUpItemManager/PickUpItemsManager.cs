using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItemsManager : ItemManager<PickUpItemsManager>
{
    [SerializeField] private float _maxRangeSpot;

    private List<PickUpItem> _pickUpItems = new List<PickUpItem>();

    public void AddPickUpItem(PickUpItemType type, Vector2 centerPos)
    {
        var item = GetPickUpItem(type.ToString());
        
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

        ResourceManager.Instance.PlayerPickUpItem(pickUpItem);
    }
}
