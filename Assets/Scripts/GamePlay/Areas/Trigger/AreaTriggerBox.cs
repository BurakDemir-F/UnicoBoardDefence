using System;
using UnityEngine;
using Utilities;

namespace GamePlay.Areas
{
    public class AreaTriggerBox : MonoBehaviour, ITriggerBox
    {
        [SerializeField] private AreaBase _area;
        public event Action<ITriggerInfo> TriggerEnter;
        public event Action<ITriggerInfo> TriggerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<ITriggerItem>(out var triggerItem))
                return;

            var triggerInfo = new TriggerInfo(_area, triggerItem);
            TriggerEnter?.Invoke(triggerInfo);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<ITriggerItem>(out var triggerItem))
                return;

            var triggerInfo = new TriggerInfo(_area, triggerItem);
            TriggerExit?.Invoke(triggerInfo);
        }
    }
}