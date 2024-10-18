using System;
using General.StateMachines;
using UnityEngine;

namespace GamePlay
{
    public abstract class GameState :MonoBehaviour, IState
    {
        [SerializeField] protected StateData _stateData;
        [SerializeField] protected GamePlayEventBus _eventBus;
        
        private ITransition _transition;
        private ICondition _condition;
        
        public ITransition Transition => _transition;
        public StateData StateData => _stateData;
        public ICondition Condition => _condition;

        protected bool isStatePlaying = false;

        public event Action<IState> ConditionMet;
        public virtual void Construct()
        {
            _transition = GetComponent<ITransition>();
            _condition = GetComponent<ICondition>();
            
            _condition.Construct();
            _condition.OnConditionMet += OnConditionMet;
        }

        public virtual void Destruct()
        {
            _condition.Destruct();
            _condition.OnConditionMet -= OnConditionMet;
        }

        public abstract void PlayState(Action<IState> onStateCompleted);

        public virtual void StopState()
        {
        }

        public virtual void MakeTransition(IState toState, Action<IState, IState> onTransitionEnd)
        {
            _transition.FromState = this;
            _transition.ToState = toState;
            _transition.MakeTransition(() => onTransitionEnd?.Invoke(this,toState));
        }

        public bool IsStatePlaying()
        {
            return isStatePlaying;
        }

        private void OnConditionMet()
        {
            ConditionMet?.Invoke(this);
        }
    }
}