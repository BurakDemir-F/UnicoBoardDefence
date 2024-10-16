using System;
using General.GridSystem;
using General.Pool.System;
using UnityEngine;

namespace GamePlay.Map.MapGrid
{
    public class DefenderBaseArea :MonoBehaviour, IGridCell,IPoolObject
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public string Key { get; set; }

        public IPool Pool { get; set; }

        public GameObject Go { get; }

        public event Action OnGetFromPool;

        public event Action OnReturnToPool;

        public void GetFromPool()
        {
            
        }

        public void ReturnedToPool()
        {
        }

        public T As<T>() where T : class, IPoolObject
        {
            return this as T;
        }
    }
}