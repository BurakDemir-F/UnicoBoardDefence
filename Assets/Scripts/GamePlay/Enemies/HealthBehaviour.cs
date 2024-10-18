using System;
using UnityEngine;

namespace GamePlay.Enemies
{
    public class HealthBehaviour : MonoBehaviour,IHealthBehaviour
    {
        private float _maxHealth;
        private float _currentHealth;

        public float Health => _currentHealth;
        public float HealthNormalized => _currentHealth / _maxHealth;
        public float MaxHealth => _maxHealth;

        public event Action Death;

        public void InitializeHealthBehaviour(float maxHealth, float currentHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = currentHealth;
        }

        public void Damage(float newHealth)
        {
            _currentHealth -= newHealth;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Death?.Invoke();
            }
        }
    }

    public interface IHealthBehaviour
    {
        void InitializeHealthBehaviour(float maxHealth, float currentHealth);
        void Damage(float newHealth);
        float Health { get; }
        float MaxHealth { get; }
        float HealthNormalized { get; }
        event Action Death;
    }
}