using System;
using System.Collections.Generic;

[Serializable]
public class ItemsWithChances
{
    public List<ItemChanceData> ItemDropChances = new List<ItemChanceData>();

    private bool _arrayIsSotred = false;

    public string GetRandomItemName()
    {
        if (!_arrayIsSotred)
        {
            SortArray(ItemDropChances, 0, ItemDropChances.Count - 1);
            _arrayIsSotred = true;
        }

        var rand = new Random();
        var chance = rand.Next(101);

        var index = 0;
        var lastIndex = ItemDropChances.Count - 1;

        do
        {
            if (chance <= ItemDropChances[index].Chance)
            {
                return ItemDropChances[index].Key;
            }

            index++;
        }
        while (index < lastIndex);

        return ItemDropChances[lastIndex].Key;
    }

    private static void SortArray(List<ItemChanceData> array, int leftIndex, int rightIndex)
    {
        var i = leftIndex;
        var j = rightIndex;
        var pivot = array[leftIndex].Chance;

        while (i <= j)
        {
            while (array[i].Chance < pivot)
            {
                i++;
            }

            while (array[j].Chance > pivot)
            {
                j--;
            }
            
            if (i <= j)
            {
                var temp = array[i];
                array[i] = array[j];
                array[j] = temp;
                i++;
                j--;
            }
        }

        if (leftIndex < j)
            SortArray(array, leftIndex, j);

        if (i < rightIndex)
            SortArray(array, i, rightIndex);
    }
}
