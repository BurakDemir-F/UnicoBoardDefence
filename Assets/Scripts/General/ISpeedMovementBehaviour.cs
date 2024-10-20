using System;
using UnityEngine;

namespace General
{
    public interface IMovementBehaviour
    {
        void SetPosition(Vector3 position);
        void Move(Vector3 start,Vector3 target, float speed);
        void Move(Vector3 target, float speed);
        void Stop();
        event Action OnDestinationReached;
    }
}