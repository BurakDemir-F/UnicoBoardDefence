using System;
using General;
using General.Pool.System;
using Unity.VisualScripting;
using UnityEngine;

namespace GamePlay.Enemies
{
    public class EnemyBase : MonoBehaviour, IPoolObject
    {
        private IMovementBehaviour _speedMovementBehaviour;
        private IHealthBehaviour _healthBehaviour;
        public string Key { get; set; }
        public IPool Pool { get; set; }
        public GameObject Go => gameObject;
        
        public event Action<EnemyBase> EnemyDeath; 
        
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        
        public void OnGetFromPool()
        {
            gameObject.SetActive(true);
            _speedMovementBehaviour = GetComponent<IMovementBehaviour>();
            _healthBehaviour = GetComponent<IHealthBehaviour>();
            _healthBehaviour.Death += OnDead;
        }

        public void OnReturnedToPool()
        {
            gameObject.SetActive(false);
            _healthBehaviour.Death -= OnDead;
        }

        public void ActivateEnemy(EnemyData enemyData ,Vector3 targetPos)
        {
            var health = enemyData.Health;
            _healthBehaviour.InitializeHealthBehaviour(health,health);
            _speedMovementBehaviour.Move(targetPos,enemyData.Speed);
        }

        private void OnDead()
        {
            EnemyDeath?.Invoke(this);
        }
        public void ReturnToPool()
        {
            Pool.Return(this);
        }

    }
}