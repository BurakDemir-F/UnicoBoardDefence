using System;

namespace General.StateMachines
{
    public interface IState : IConstructionProvider
    {
        ITransition Transition { get; }
        StateData StateData {get;}
        ICondition Condition { get; }
        void PlayState(Action<IState> onStateCompleted);
        void StopState();
        bool IsStatePlaying();
        event Action<IState> ConditionMet;
        void MakeTransition(IState toState, Action<IState, IState> onTransitionEnd);
    }
}