using System;
using UnityEngine;

namespace General.Pool.System
{
    public interface IPoolObject
    {
        string Key { get; set; }
        void OnGetFromPool();
        void OnReturnedToPool();
        IPool Pool { get; set; }
        GameObject Go { get; }
        void ReturnToPool();
    }
}