using System;
using GamePlay.Enemies;
using UnityEngine;

namespace GamePlay.Defenders.Weapons
{
    public class BulletTrigger : MonoBehaviour
    {
        public event Action<IDamageable> Hit;
        private bool _isActive;
        private void OnTriggerEnter(Collider other)
        {
            if(!_isActive)
                return;
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                Hit?.Invoke(damageable);
            }
        }

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }
    }
}