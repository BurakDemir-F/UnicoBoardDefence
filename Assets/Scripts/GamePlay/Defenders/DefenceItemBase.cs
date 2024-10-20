using System.Collections.Generic;
using General.Pool.System;
using UnityEngine;

namespace Defenders
{
    public abstract class DefenceItemBase : MonoBehaviour,IPoolObject,IAreaPlaceable
    {
        private Weapon _weapon;
        public DefenderData DefenderData => _defenderData;
        public string Key { get; set; }
        public IPool Pool { get; set; }
        public GameObject Go => gameObject;
        public DefenceItemBase DefenceItem => this;
        
        protected DefenderData _defenderData;
        private IMaterialChanger _materialChanger;
        private IPoolCollection _poolCollection;

        public void OnGetFromPool()
        {
            _materialChanger = GetComponent<IMaterialChanger>();
            _weapon = GetComponent<Weapon>();
            gameObject.SetActive(true);
        }

        public void OnReturnedToPool()
        {
            gameObject.SetActive(false);
        }

        public void Initialize(DefenderData data,IPoolCollection poolCollection)
        {
            _defenderData = data;
            _poolCollection = poolCollection;
            _weapon.Initialize(_defenderData.WeaponData,_poolCollection);
        }

        public void AddTarget(Transform target)
        {
            _weapon.AddTarget(target);
        }

        public void RemoveTarget(Transform target)
        {
            _weapon.RemoveTarget(target);
        }

        public void Place(Vector3 position)
        {
            transform.position = position;
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