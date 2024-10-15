using System;

namespace General.StateMachines
{
    public interface IState : IConstructionProvider
    {
        IStateTransition Transition { get; }
        StateData StateData {get;}
        ICondition StartCondition { get; }
        void PlayState(Action<IState> onStateCompleted);
        void StopState();
        bool IsStatePlaying();
        event Action<IState> OnStateConditionMet;
        void MakeTransition(IState toState, Action<IState, IState> onTransitionEnd);
    }
}