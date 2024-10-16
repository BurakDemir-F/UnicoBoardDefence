using General.Pool.System;
using UnityEngine;

namespace General.Pool.Data
{
    [global::System.Serializable]
    public class PoolConfig
    {
        public GameObject PoolObjectPrefab;
        public int DefaultCapacity;
        public string PoolKey;
        public PoolObjectCreator Creator;
    }
}