using System.Collections.Generic;

namespace General.StateMachines
{
    public interface IStateMachine : IConstructionProvider
    {
        void SetStates(IEnumerable<IState> states);
        void SetStartState(IState startState);
        bool AddState(IState state);
        bool RemoveState(IState state);
        void StartMachine();
        void EndMachine();
    }
}