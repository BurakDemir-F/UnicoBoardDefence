using GamePlay.Defenders.Weapons;
using GamePlay.Enemies;
using General.Pool.System;
using UnityEngine;

namespace GamePlay.Defenders
{
    public abstract class DefenceItemBase : MonoBehaviour, IPoolObject, IAreaPlaceable
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
            _weapon.ClearAllTargets();
            gameObject.SetActive(false);
        }

        public void Initialize(DefenderData data, IPoolCollection poolCollection)
        {
            _defenderData = data;
            _poolCollection = poolCollection;
            _weapon.Initialize(_defenderData.WeaponData, _poolCollection);
        }

        public void AddTarget(IEnemy target)
        {
            _weapon.AddTarget(target);
        }

        public void RemoveTarget(IEnemy target)
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

        public bool IsTrackingEnemy(IEnemy enemy)
        {
            return _weapon.HasTarget(enemy);
        }
    }

    public interface IAreaPlaceable
    {
        void Place(Vector3 position);
        DefenceItemBase DefenceItem { get; }
    }
}