using System.Collections.Generic;

public static class PickUpItemsChancesSorter
{
    public static List<PickUpItemChanceData> SortArray(List<PickUpItemChanceData> array, int leftIndex, int rightIndex)
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

        return array;
    }
}
