using System.Collections.Generic;
using GamePlay;
using General.StateMachines;
using UnityEngine;

namespace Controllers
{
    public class GamePlayController : MonoBehaviour
    {
        [SerializeField] private List<GameState> _gamePlayStates;
        [SerializeField] private GameState _startState;
        private StateMachine _gamePlayStateMachine;

        private void Awake()
        {
            _gamePlayStateMachine = new StateMachine();
            _gamePlayStateMachine.SetStates(_gamePlayStates);
            _gamePlayStateMachine.SetStartState(_startState);
            _gamePlayStateMachine.Construct();
            _gamePlayStateMachine.StartMachine();
        }

        private void OnDestroy()
        {
            _gamePlayStateMachine.Destruct();
        }
    }
}