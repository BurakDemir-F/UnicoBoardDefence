using General.Pool.System;
using UnityEngine;

namespace GamePlay.Enemies
{
    public class EnemyBase : MonoBehaviour, IPoolObject
    {
        private IMovementBehaviour _movementBehaviour;
        private IHealthBehaviour _healthBehaviour;
        public string Key { get; set; }
        public IPool Pool { get; set; }
        public GameObject Go => gameObject;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        
        public void GetFromPool()
        {
            gameObject.SetActive(true);
            _movementBehaviour = GetComponent<IMovementBehaviour>();
            _healthBehaviour = GetComponent<IHealthBehaviour>();
        }

        public void ReturnedToPool()
        {
            gameObject.SetActive(false);
        }

        public void ActivateEnemy(EnemyData enemyData ,Vector3 targetPos)
        {
            var health = enemyData.Health;
            _healthBehaviour.InitializeHealthBehaviour(health,health);
            _movementBehaviour.Move(targetPos,enemyData.Speed);
        }
    }
}