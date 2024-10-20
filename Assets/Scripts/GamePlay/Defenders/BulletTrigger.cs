using System;
using GamePlay.Enemies;
using UnityEngine;

namespace Defenders
{
    public class BulletTrigger : MonoBehaviour
    {
        public event Action<IDamageable> Hit;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                Hit?.Invoke(damageable);
            }
        }
    }
}