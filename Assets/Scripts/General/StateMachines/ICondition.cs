using System;

namespace General.StateMachines
{
    public interface ICondition
    {
        bool IsConditionMet { get; }
        event Action OnConditionMet;
    }
}