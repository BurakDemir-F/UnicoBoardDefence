using System;
using General.StateMachines;
using UnityEngine;

namespace GamePlay
{
    public abstract class GameStateCondition : MonoBehaviour,ICondition
    {
        protected bool isConditionMet;
        public bool IsConditionMet => isConditionMet;
        public event Action OnConditionMet;

        public virtual void Construct()
        {
            
        }

        public virtual void Destruct()
        {
            
        }

        protected void InvokeConditionMetEvent()
        {
            OnConditionMet?.Invoke();
        }
    }
}