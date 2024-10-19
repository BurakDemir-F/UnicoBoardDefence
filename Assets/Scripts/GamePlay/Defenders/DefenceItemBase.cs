using System.Collections.Generic;
using GamePlay.Areas;
using General.Pool.System;
using UnityEngine;

namespace Defenders
{
    public class DefenceItemBase : MonoBehaviour,IPoolObject,IAreaPlaceable
    {
        private DefenderData _defenderData;
        public DefenderData DefenderData => _defenderData;
        public string Key { get; set; }
        public IPool Pool { get; set; }
        public GameObject Go => gameObject;
        private HashSet<Transform> _targets;

        public void GetFromPool()
        {
            _targets = new HashSet<Transform>();
        }

        public void ReturnedToPool()
        {
            
        }

        public void AddTarget(Transform target)
        {
            _targets.Add(target);
        }

        public void RemoveTarget(Transform target)
        {
            _targets.Remove(target);
        }
        
        public void Place(Vector3 position)
        {
            transform.position = position;
        }
    }

    public interface IAreaPlaceable
    {
        void Place(Vector3 position);
    }
}