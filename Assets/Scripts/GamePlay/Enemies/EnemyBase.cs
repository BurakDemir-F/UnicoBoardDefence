using System;
using General;
using General.Pool.System;
using UnityEngine;

namespace GamePlay.Enemies
{
    public class EnemyBase : MonoBehaviour, IPoolObject, IEnemy
    {
        private IMovementBehaviour _movementBehaviour;
        private IHealthBehaviour _healthBehaviour;
        public string Key { get; set; }
        public IPool Pool { get; set; }
        public GameObject Go => gameObject;
        
        public event Action<IEnemy> EnemyDeath; 
        
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Transform Transform => transform;
        
        public void OnGetFromPool()
        {
            gameObject.SetActive(true);
            _movementBehaviour = GetComponent<IMovementBehaviour>();
            _healthBehaviour = GetComponent<IHealthBehaviour>();
            _healthBehaviour.Death += OnDead;
        }

        public void OnReturnedToPool()
        {
            gameObject.SetActive(false);
            _healthBehaviour.Death -= OnDead;
            _movementBehaviour.Stop();
        }

        public void ActivateEnemy(EnemyData enemyData ,Vector3 startPos,Vector3 targetPos, float oneAreaLength)
        {
            var health = enemyData.Health;
            _healthBehaviour.InitializeHealthBehaviour(health,health);
            _movementBehaviour.Move(startPos,targetPos,enemyData.Speed * oneAreaLength);
            transform.forward = targetPos - startPos;
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