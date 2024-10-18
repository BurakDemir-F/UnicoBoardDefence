using System;
using UnityEngine;

namespace General.Pool.System
{
    public interface IPoolObject
    {
        string Key { get; set; }
        void GetFromPool();
        void ReturnedToPool();
        IPool Pool { get; set; }
        GameObject Go { get; }
    }
}