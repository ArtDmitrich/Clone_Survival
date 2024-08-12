using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public static class ListExtension
{
    public static void Shuffle<T>(this List<T> list)
    {
        var random = new Random();
        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            var value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
