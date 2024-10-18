using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class ListExtensions
    {
        public static T GetRandom<T>(this List<T> items)
        {
            var index = Random.Range(0, items.Count);
            return items[index];
        }
    }
}