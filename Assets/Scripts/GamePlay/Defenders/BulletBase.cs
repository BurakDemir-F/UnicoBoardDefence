using GamePlay.Enemies;
using General;
using General.Pool.System;
using UnityEngine;

namespace Defenders
{
    public class BulletBase : MonoBehaviour,IPoolObject
    {
        private IMovementBehaviour _movementBehaviour;
        public string Key { get; set; }
        public IPool Pool { get; set; }
        public GameObject Go => gameObject;

        public void OnGetFromPool()
        {
            gameObject.SetActive(true);
            _movementBehaviour = GetComponent<IMovementBehaviour>();
        }

        public void OnReturnedToPool()
        {
            gameObject.SetActive(false);
        }

        public void ReturnToPool()
        {
            Pool.Return(this);
        }
        
        public void Fire(Transform target,float speed)
        {
            _movementBehaviour.Move(target.position,speed);
        }
    }
}