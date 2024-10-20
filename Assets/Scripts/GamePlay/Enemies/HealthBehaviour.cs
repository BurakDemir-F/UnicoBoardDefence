using System;
using UnityEngine;
using Utilities;

namespace GamePlay.Enemies
{
    public class HealthBehaviour : MonoBehaviour,IHealthBehaviour
    {
        private IHealthUI _healthUI;
        private float _maxHealth;
        private float _currentHealth;

        public float Health => _currentHealth;
        public float HealthNormalized => _currentHealth / _maxHealth;
        public float MaxHealth => _maxHealth;

        public event Action Death;

        public void InitializeHealthBehaviour(float maxHealth, float currentHealth)
        {
            _healthUI = GetComponent<IHealthUI>();
            _maxHealth = maxHealth;
            _currentHealth = currentHealth;
            _healthUI.SetHealth(HealthNormalized);
        }

        public void Damage(float damageAmount)
        {
            _currentHealth -= damageAmount;
            _healthUI.SetHealth(HealthNormalized);
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
        void Damage(float damageAmount);
        float Health { get; }
        float MaxHealth { get; }
        float HealthNormalized { get; }
        event Action Death;
    }
}