using System;


namespace General.StateMachines
{
    public interface ITransition
    {
        void MakeTransition(Action OnTransitionEnd);
        IState FromState { get; set; }
        IState ToState { get; set; }
    }
}