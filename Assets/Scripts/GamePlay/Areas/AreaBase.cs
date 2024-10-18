using System;
using General.GridSystem;
using General.Pool.System;
using UnityEngine;

namespace GamePlay.Areas
{
    public class AreaBase : MonoBehaviour,IGridCell,IPoolObject
    {
        [SerializeField] private Transform _center;
        
        private IAreaAnimator _areaAnimator;
        public int XPos { get; set; }
        public int YPos { get; set; }
        public string Key { get; set; }
        public IPool Pool { get; set; }
        public GameObject Go => gameObject;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Vector3 CenterPosition
        {
            get => _center.position;
        }
        
        public void GetFromPool()
        {
            gameObject.SetActive(true);
            _areaAnimator = GetComponent<IAreaAnimator>();
        }


        public void ReturnedToPool()
        {
            
        }

        public void AnimatePlacing(Action onComplete)
        {
            _areaAnimator.AnimatePlacing(onComplete);
        }
    }
}