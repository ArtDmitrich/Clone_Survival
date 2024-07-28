using System.Collections.Generic;
using UnityEngine;

public class PickUpItemsManager : ItemManager<PickUpItemsManager>
{
    [Tooltip("The sum of all chances must not exceed 100. At the beginning of each game, the list is sorted from lowest to highest chance.")]
    [SerializeField] private List<PickUpItemChanceData> _pickUpItemDropChances = new List<PickUpItemChanceData>();
    [SerializeField] private float _maxRangeSpot;

    private readonly List<PickUpItem> _pickUpItems = new List<PickUpItem>();

    private void Start()
    {
        PickUpItemsChancesSorter.SortArray(_pickUpItemDropChances, 0, _pickUpItemDropChances.Count - 1);
    }

    public void AddPickUpItem(Vector2 centerPos)
    {
        var item = GetPickUpItem(GetRandomPickUpItemName());
        
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

    private string GetRandomPickUpItemName()
    {
        var chance = Random.Range(1, 101);

        var index = 0;
        var lastIndex = _pickUpItemDropChances.Count - 1;

        do
        {
            if (chance <= _pickUpItemDropChances[index].Chance)
            {
                return _pickUpItemDropChances[index].Key;
            }

            index++;
        }
        while (index < lastIndex);

        return _pickUpItemDropChances[lastIndex].Key;
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
