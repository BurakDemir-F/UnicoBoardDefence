using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace General.StateMachines
{
    public class StateMachine : IStateMachine
    {
        private HashSet<IState> _states;
        private Queue<IState> _stateQueue;
        private IState _startState;
        private IState _currentState;

        public StateMachine()
        {
            _states = new();
            _stateQueue = new();
        }
        public void Construct()
        {
            foreach (var state in _states)
            {
                state.Construct();
                state.ConditionMet += OnStateConditionMet;
            }
        }

        public void Destruct()
        {
            foreach (var state in _states)
            {
                state.Destruct();
                state.ConditionMet -= OnStateConditionMet;
            }
        }
        
        public void SetStates(IEnumerable<IState> states)
        {
            _states.Clear();
            foreach (var state in states)
            {
                _states.Add(state);
            }
        }

        public void SetStartState(IState startState)
        {
            _startState = startState;
            _states.Add(startState);
        }

        public bool AddState(IState state)
        {
            return _states.Add(state);
        }

        public bool RemoveState(IState state)
        {
            return _states.Remove(state);
        }

        public void StartMachine()
        {
            $"machine started: {_startState}".PrintColored(Color.white);
            _currentState = _startState;
            _startState.PlayState(OnStateCompleted);
        }

        public void EndMachine()
        {
            if (_currentState.IsStatePlaying())
            {
                _currentState.StopState();
            }
        }

        private void OnStateConditionMet(IState state)
        {
            var isPlaying = _currentState.IsStatePlaying();
            var shouldFinish = _currentState.StateData.ShouldFinish;
            
            if (isPlaying && !shouldFinish)
            {
                _currentState.StopState();
                _currentState.MakeTransition(state,OnTransitionCompleted);
                return;
            }
            
            if (isPlaying)
            {
                _stateQueue.Enqueue(state);
                return;
            }
            
            _currentState.MakeTransition(state,OnTransitionCompleted);
        }

        private void OnStateCompleted(IState state)
        {
            $"State completed: {state}".PrintColored(Color.green);
            
            if (_currentState != state)
            {
                $"Something wrong here, unstarted state looking like finished current state: {_currentState}, finished state:{state}".PrintColored(Color.red);
                return;
            }

            if (_stateQueue.Count > 0)
            {
                var nextState = _stateQueue.Dequeue();
                _currentState.MakeTransition(nextState,OnTransitionCompleted);
            }
        }

        private void OnTransitionCompleted(IState from, IState to)
        {
            _currentState = to;
            $"new state playing: {_currentState}".PrintColored(Color.white);
            _currentState.PlayState(OnStateCompleted);
        }
    }
}