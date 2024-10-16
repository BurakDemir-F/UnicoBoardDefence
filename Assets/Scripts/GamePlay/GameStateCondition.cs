using System;
using General.StateMachines;

namespace GamePlay
{
    public abstract class GameStateCondition : ICondition
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