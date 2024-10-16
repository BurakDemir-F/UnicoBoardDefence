using System.Collections.Generic;
using General;
using General.StateMachines;
using UnityEngine;

namespace GamePlay.TutorialStates
{
    public class TutorialStateManager : MonoBehaviour,IConstructionProvider
    {
        [SerializeField] private List<GameState> _states;
        [SerializeField] private GameState _startState;
        private IStateMachine _stateMachine;
        
        public void Construct()
        {
            _stateMachine = new StateMachine();
            _stateMachine.SetStates(_states);
            _stateMachine.SetStartState(_startState);
        }

        public void Destruct()
        {
            
        }
    }

    public interface IStateManager
    {
        void StartStateManager();
    }
}