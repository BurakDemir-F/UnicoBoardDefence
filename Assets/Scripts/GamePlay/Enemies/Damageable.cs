using UnityEngine;

namespace GamePlay.Enemies
{
    public class Damageable : MonoBehaviour,IDamageable
    {
        [SerializeField] private HealthBehaviour _healthBehaviour;
        public void Damage(float damage)
        {
            _healthBehaviour.Damage(damage);
        }
    }
}