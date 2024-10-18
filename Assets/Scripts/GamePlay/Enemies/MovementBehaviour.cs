using System;
using UnityEngine;
using Utilities;

namespace GamePlay.Enemies
{
    public class MovementBehaviour : MonoBehaviour,IMovementBehaviour
    {
        public event Action OnDestinationReached;
        private MovementInfoTemp _movementTemp;
        private Vector3 _defaultPos;
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Move(Vector3 target, float speed)
        {
            _defaultPos = transform.position;
            var defaultHeight = _defaultPos.y;
            _movementTemp.Speed = speed;
            _movementTemp.Target = target.SetY(defaultHeight);
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

        private struct MovementInfoTemp
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

    public interface IMovementBehaviour
    {
        void SetPosition(Vector3 position);
        void Move(Vector3 target, float speed);
        void Stop();
        event Action OnDestinationReached;
    }
}