using System;
using General.Counter;
using General.StateMachines;
using UnityEngine;

namespace GamePlay.GamePlayStates.LevelFail
{
    public class LevelFail : GameState
    {
        [SerializeField] private LevelEndUI _failUI;
        [SerializeField] private float _uiShowDuration = 2f;
        private ICounter _counter;
        private Action<IState> _onStateCompleted;
        public override void Construct()
        {
            base.Construct();
            _counter = GetComponent<ICounter>();
        }
        public override void PlayState(Action<IState> onStateCompleted)
        {
            _failUI.Activate();
            _eventBus.Publish(GamePlayEvent.LevelFail,null);
            _counter.Count(_uiShowDuration, null, OnUIShown);
            _onStateCompleted = onStateCompleted;
        }

        private void OnUIShown()
        {
            _failUI.Deactivate();
            _onStateCompleted?.Invoke(this);
            _eventBus.Publish(GamePlayEvent.LevelEnd,null);
        }
    }
}