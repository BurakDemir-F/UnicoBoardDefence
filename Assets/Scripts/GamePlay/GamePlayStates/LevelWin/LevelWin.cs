using System;
using GamePlay.EventBus;
using General.Counter;
using General.StateMachines;
using UnityEngine;

namespace GamePlay.GamePlayStates.LevelWin
{
    public class LevelWin : GameState
    {
        [SerializeField] private LevelEndUI _winUI;
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
            _winUI.Activate();
            _eventBus.Publish(GamePlayEvent.LevelWin,null);
            _counter.Count(_uiShowDuration, null, OnUIShown);
            _onStateCompleted = onStateCompleted;
        }

        private void OnUIShown()
        {
            _winUI.Deactivate();
            _onStateCompleted?.Invoke(this);
            _eventBus.Publish(GamePlayEvent.LevelEnd,null);
        }
    }
}