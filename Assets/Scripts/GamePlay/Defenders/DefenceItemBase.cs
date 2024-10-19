using System.Collections.Generic;
using General.Pool.System;
using UnityEngine;

namespace Defenders
{
    public class DefenceItemBase : MonoBehaviour,IPoolObject,IAreaPlaceable
    {
        public DefenderData DefenderData => _defenderData;
        public string Key { get; set; }
        public IPool Pool { get; set; }
        public GameObject Go => gameObject;

        private DefenderData _defenderData;
        private IMaterialChanger _materialChanger;
        private HashSet<Transform> _targets;

        public DefenceItemBase DefenceItem => this;

        public void GetFromPool()
        {
            _targets = new HashSet<Transform>();
            _materialChanger = GetComponent<IMaterialChanger>();
            gameObject.SetActive(true);
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


        public void SetData(DefenderData data)
        {
            _defenderData = data;
        }

        public void MakeTransparent()
        {
            _materialChanger.MakeTransparent();
        }

        public void MakeOpaque()
        {
            _materialChanger.MakeOpaque();
        }
    }

    public interface IAreaPlaceable
    {
        void Place(Vector3 position);
        DefenceItemBase DefenceItem { get; }
    }
}