using System;
using General;
using UnityEngine;
using Utilities;

namespace GamePlay.Enemies
{
    public class MovementBehaviour : MonoBehaviour,IMovementBehaviour
    {
        public event Action OnDestinationReached;
        protected MovementInfoTemp _movementTemp;
        protected Vector3 _defaultPos;
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Move(Vector3 start,Vector3 target, float speed)
        {
            _defaultPos = start;
            _movementTemp.Target = target;
            _movementTemp.Speed = speed;
            _movementTemp.IsMoving = true;
            _movementTemp.Duration = Vector3.Magnitude(target - _defaultPos) / speed;
        }

        public void Move(Vector3 target, float speed)
        {
            _defaultPos = transform.position;
            _movementTemp.Target = target;
            _movementTemp.Speed = speed;
            _movementTemp.IsMoving = true;
            _movementTemp.Duration = Vector3.Magnitude(target - _defaultPos) / speed;
        }

        public void Stop()
        {
            _movementTemp.Refresh();
        }

        private void Update()
        {
            if(!_movementTemp.IsMoving)
                return;
            
            var tValue = _movementTemp.Counter / _movementTemp.Duration;
            var nextPos = Vector3.Lerp(_defaultPos, _movementTemp.Target, tValue);
            transform.position = nextPos;
            _movementTemp.Counter += Time.deltaTime;

            if (_movementTemp.Counter >= _movementTemp.Duration)
            {
                _movementTemp.Refresh();
                OnDestinationReached?.Invoke();
            }
        }

        protected struct MovementInfoTemp
        {
            public bool IsMoving;
            public Vector3 Target;
            public float Speed;
            public float Duration;
            public float Counter;
            public void Refresh()
            {
                IsMoving = false;
                Target = default;
                Speed = 0f;
                Duration = 0f;
                Counter = 0f;
            }
        }
    }
}