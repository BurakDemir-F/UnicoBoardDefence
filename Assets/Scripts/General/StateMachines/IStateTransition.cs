using System;


namespace General.StateMachines
{
    public interface IStateTransition : IConstructionProvider
    {
        void MakeTransition(Action OnTransitionEnd);
        IState FromState { get; set; }
        IState ToState { get; set; }
    }
}