using System;
using System.Collections.Generic;
using General.Counter;
using UnityEngine;

namespace General.Behaviours
{
    public class CounterSingleton : Singleton<CounterSingleton>
    {
        private readonly List<CounterVo> _counters = new ();
        private readonly HashSet<Action> _updateActions = new();
        public event Action OnUpdate;

        public void AddUpdateAction(Action updateAction)
        {
            var isAdded = _updateActions.Add(updateAction);
            if (isAdded)
                OnUpdate += updateAction;
        }

        public void RemoveUpdateAction(Action updateAction)
        {
            var isRemoved = _updateActions.Remove(updateAction);
            if (isRemoved)
                OnUpdate -= updateAction;
        }
        
        public void AddCounter(CounterVo vo)
        {
            _counters.Add(vo);
        }

        public void RemoveCounter(CounterVo vo)
        {
            _counters.Remove(vo);
        }
        
        private void Update()
        {
            UpdateCounters();
            InvokeUpdateActions();
        }

        private void InvokeUpdateActions()
        {
            OnUpdate?.Invoke();
        }
        
        private void UpdateCounters()
        {
            var count = _counters.Count;
            var index = count - 1;
            while (index >= 0)
            {
                var currentCounter = _counters[index];
                currentCounter.Counter += Time.deltaTime;
                var value = Mathf.Clamp01(currentCounter.Counter / currentCounter.Duration);
                currentCounter.InvokeUpdateAction(value);
                if (currentCounter.IsFinished())
                {
                    if (currentCounter.CounterType is CounterType.Once or CounterType.None)
                    {
                        _counters.RemoveAt(index);
                    }
                    
                    currentCounter.InvokeFinishAction();
                    currentCounter.Refresh();
                }
                
                index--;
            }
        }
    }
}