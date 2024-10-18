using System;
using General.GridSystem;
using General.Pool.System;
using UnityEngine;

namespace GamePlay.Areas
{
    public class AreaBase : MonoBehaviour,IGridCell,IPoolObject
    {
        private IAreaAnimator _areaAnimator;
        
        public int XPos { get; set; }
        public int YPos { get; set; }
        public string Key { get; set; }
        public IPool Pool { get; set; }
        public GameObject Go => gameObject;
        public event Action OnGetFromPool;
        public event Action OnReturnToPool;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        
        public void GetFromPool()
        {
            gameObject.SetActive(true);
            _areaAnimator = GetComponent<IAreaAnimator>();
        }


        public void ReturnedToPool()
        {
            
        }

        public T As<T>() where T : class, IPoolObject
        {
            return this as T;
        }

        public void AnimatePlacing(Action onComplete)
        {
            _areaAnimator.AnimatePlacing(onComplete);
        }
    }
}