using System.Collections.Generic;
using General.Pool.System;
using UnityEngine;

namespace Defenders
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _attackStart;
        private List<Transform> _targets = new();
        private WeaponData _weaponData;
        private AttackInfo _attackInfo;
        private IPoolCollection _poolCollection;

        public void Initialize(WeaponData weaponData,IPoolCollection poolCollection)
        {
            _weaponData = weaponData;
            _poolCollection = poolCollection;
            _attackInfo.Reset();
        }

        public void AddTarget(Transform target)
        {
            HandleNewTarget(target);
        }

        public void RemoveTarget(Transform target)
        {
            HandleTargetRemove(target);
        }

        private void HandleNewTarget(Transform target)
        {
            if (!_attackInfo.IsAttacking)
            {
                _attackInfo.IsAttacking = true;
                _attackInfo.CurrentTarget = target;
            }

            _targets.Add(target);
        }

        private void HandleTargetRemove(Transform target)
        {
            _targets.Remove(target);
            if (_targets.Count == 0)
            {
                _attackInfo.Reset();
                return;
            }

            var latestTarget = _targets[^1];
            _attackInfo.CurrentTarget = latestTarget;
            _attackInfo.IsAttacking = true;
        }

        private void Update()
        {
            _attackInfo.ElapsedTime += Time.deltaTime;
            
            if (!_attackInfo.IsAttacking)
                return;

            var timeDifference = _attackInfo.ElapsedTime - _attackInfo.LatestAttackTime;
            if (timeDifference >= _weaponData.AttackInterval)
            {
                var bullet = _poolCollection.Get<BulletBase>(_weaponData.BulletKey);
                var startPos = _attackStart.position;
                bullet.Go.transform.position = startPos;
                var bulletTarget = _attackInfo.CurrentTarget;
                bullet.Fire(bulletTarget,_weaponData.BulletSpeed);
                _attackInfo.LatestAttackTime = _attackInfo.ElapsedTime;
            }
        }

        private struct AttackInfo
        {
            public bool IsAttacking;
            public Transform CurrentTarget;
            public float ElapsedTime;
            public float LatestAttackTime;

            public void Refresh()
            {
                IsAttacking = false;
                CurrentTarget = null;
            }

            public void Reset()
            {
                Refresh();
                ElapsedTime = 0f;
                LatestAttackTime = 0f;
            }
        }
    }
}