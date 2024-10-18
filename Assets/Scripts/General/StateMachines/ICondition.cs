using System;

namespace General.StateMachines
{
    public interface ICondition
    {
        void Construct();
        void Destruct();
        bool IsConditionMet { get; }
        event Action OnConditionMet;
    }
}