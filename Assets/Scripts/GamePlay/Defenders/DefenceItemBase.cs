using System.Collections.Generic;
using General.Pool.System;
using UnityEngine;

namespace Defenders
{
    public abstract class DefenceItemBase : MonoBehaviour,IPoolObject,IAreaPlaceable
    {
        public DefenderData DefenderData => _defenderData;
        public string Key { get; set; }
        public IPool Pool { get; set; }
        public GameObject Go => gameObject;

        protected DefenderData _defenderData;
        protected HashSet<Transform> _targets;
        
        private IMaterialChanger _materialChanger;

        public DefenceItemBase DefenceItem => this;

        public void OnGetFromPool()
        {
            _targets = new HashSet<Transform>();
            _materialChanger = GetComponent<IMaterialChanger>();
            gameObject.SetActive(true);
        }

        public void OnReturnedToPool()
        {
            gameObject.SetActive(false);
        }

        public void AddTarget(Transform target)
        {
            _targets.Add(target);
            HandleTargets();
        }

        public void RemoveTarget(Transform target)
        {
            _targets.Remove(target);
            HandleTargets();
        }

        protected abstract void HandleTargets();
        
        public void Place(Vector3 position)
        {
            transform.position = position;
        }


        public void SetData(DefenderData data)
        {
            _defenderData = data;
        }

        public void MakeTransparent()
        {
            _materialChanger.MakeTransparent();
        }

        public void MakeOpaque()
        {
            _materialChanger.MakeOpaque();
        }
        
        public void ReturnToPool()
        {
            Pool.Return(this);
        }
    }

    public interface IAreaPlaceable
    {
        void Place(Vector3 position);
        DefenceItemBase DefenceItem { get; }
    }
}