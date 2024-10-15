using System;

namespace General.StateMachines
{
    public interface ICondition : IConstructionProvider
    {
        bool IsConditionMet { get; }
        event Action OnConditionMet;
    }
}