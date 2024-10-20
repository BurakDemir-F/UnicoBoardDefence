﻿using System;
using General.GridSystem;
using General.Pool.System;
using UnityEngine;

namespace GamePlay.Areas
{
    public class AreaBase : MonoBehaviour,IGridCell,IPoolObject
    {
        [SerializeField] private Transform _center;
        [SerializeField]private AreaType _areaType;
        private IAreaAnimator _areaAnimator;
        public int XPos { get; set; }
        public int YPos { get; set; }
        public string Key { get; set; }
        public IPool Pool { get; set; }
        public GameObject Go => gameObject;
        public AreaType AreaType => _areaType;
        
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Vector3 CenterPosition
        {
            get => _center.position;
        }
        
        public virtual void OnGetFromPool()
        {
            gameObject.SetActive(true);
            _areaAnimator = GetComponent<IAreaAnimator>();
        }


        public virtual void OnReturnedToPool()
        {
            gameObject.SetActive(false);
        }

        public void AnimatePlacing(Action onComplete)
        {
            _areaAnimator.AnimatePlacing(onComplete);
        }

        public void ReturnToPool()
        {
            Pool.Return(this);
        }

        
        private void OnDrawGizmosSelected()
        {
            if(_center == null)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_center.position,.5f);
        }
    }
}