using UnityEngine;

namespace Utilities
{
    public static class ObjectExtensions
    {
        public static void Print(this object obj, GameObject go)
        {
            Debug.Log(obj,go);
        }

        public static void Print(this object obj)
        {
            Debug.Log(obj);
        }
    }
}