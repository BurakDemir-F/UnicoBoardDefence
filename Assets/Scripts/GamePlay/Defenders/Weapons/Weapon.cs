using System.Collections.Generic;
using GamePlay.Enemies;
using General.Pool.System;
using UnityEngine;

namespace GamePlay.Defenders.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _attackStart;
        private List<IEnemy> _targets = new();
        private WeaponData _weaponData;
        private AttackInfo _attackInfo;
        private IPoolCollection _poolCollection;

        public void Initialize(WeaponData weaponData,IPoolCollection poolCollection)
        {
            _weaponData = weaponData;
            _poolCollection = poolCollection;
            _attackInfo.Reset();
        }

        public void AddTarget(IEnemy target)
        {
            HandleNewTarget(target);
        }

        public void RemoveTarget(IEnemy target)
        {
            HandleTargetRemove(target);
        }

        public void ClearAllTargets()
        {
            _attackInfo.Reset();
            _targets.Clear();
        }

        public bool HasTarget(IEnemy target)
        {
            return _targets.Contains(target);
        }

        private void HandleNewTarget(IEnemy enemy)
        {
            enemy.EnemyDeath += OnEnemyDeath;
            if (!_attackInfo.IsAttacking)
            {
                _attackInfo.IsAttacking = true;
                _attackInfo.CurrentTarget = enemy;
            }

            _targets.Add(enemy);
        }

        private void HandleTargetRemove(IEnemy target)
        {
            _targets.Remove(target);
            if (_targets.Count == 0)
            {
                _attackInfo.Refresh();
                return;
            }

            if (_targets.Count > 0 || target == _attackInfo.CurrentTarget)
            {
                _attackInfo.Refresh();
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
                var bulletTarget = _attackInfo.CurrentTarget.Transform;
                bullet.Fire(bulletTarget,_weaponData.BulletSpeed);
                bullet.Hit += OnBulletHit;
                bullet.DestinationReached += OnBulletDestinationReached;
                _attackInfo.LatestAttackTime = _attackInfo.ElapsedTime;
            }
        }

        private void OnBulletHit(IDamageable damageable,BulletBase bullet)
        {
            bullet.Hit -= OnBulletHit;
            bullet.DestinationReached -= OnBulletDestinationReached;
            damageable.Damage(_weaponData.Damage);
            bullet.ReturnToPool();
        }

        private void OnBulletDestinationReached(BulletBase bullet)
        {
            bullet.Hit -= OnBulletHit;
            bullet.DestinationReached -= OnBulletDestinationReached;
            bullet.ReturnToPool();
        }

        private void OnEnemyDeath(IEnemy enemy)
        {
            enemy.EnemyDeath -= OnEnemyDeath;
            HandleTargetRemove(enemy);
        }
        
        private struct AttackInfo
        {
            public bool IsAttacking;
            public IEnemy CurrentTarget;
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
                LatestAttackTime = -10f;
            }
        }

        private void OnDrawGizmos()
        {
            if (_attackStart != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_attackStart.position, .1f);
            }

            if (_targets != null)
            {
                foreach (var target in _targets)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(target.Transform.position, .1f);
                }
            }
        }
    }
}