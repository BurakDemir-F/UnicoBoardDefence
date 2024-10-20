using System;
using GamePlay.Enemies;
using General;
using General.Pool.System;
using UnityEngine;

namespace Defenders
{
    public class BulletBase : MonoBehaviour, IPoolObject
    {
        [SerializeField] private BulletTrigger _bulletTrigger;
        private IMovementBehaviour _movementBehaviour;
        public string Key { get; set; }
        public IPool Pool { get; set; }
        public GameObject Go => gameObject;
        public event Action<IDamageable,BulletBase> Hit;
        public event Action<BulletBase> DestinationReached;

        public void OnGetFromPool()
        {
            gameObject.SetActive(true);
            _movementBehaviour = GetComponent<IMovementBehaviour>();
            _bulletTrigger.Hit += HandleHit;
            _bulletTrigger.Activate();
            _movementBehaviour.OnDestinationReached += OnDestinationReached;
        }

        public void OnReturnedToPool()
        {
            transform.position = Vector3.one * -1000f;
            gameObject.SetActive(false);
            _movementBehaviour.OnDestinationReached += OnDestinationReached;
        }

        public void ReturnToPool()
        {
            Pool.Return(this);
        }

        private void HandleHit(IDamageable damageable)
        {
            _bulletTrigger.Deactivate();
            _movementBehaviour.Stop();
            Hit?.Invoke(damageable,this);
        }

        private void OnDestinationReached()
        {
            DestinationReached?.Invoke(this);
        }

        public void Fire(Transform target, float speed)
        {
            _movementBehaviour.Move(GetTarget(target), speed);
        }

        private Vector3 GetTarget(Transform target)
        {
            var pos = transform.position;
            var direction = target.position - pos;
            return pos + direction.normalized * 50f;
        }
    }
}