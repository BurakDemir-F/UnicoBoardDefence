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
        public override void Construct()
        {
            base.Construct();
            _counter = GetComponent<ICounter>();
        }
        public override void PlayState(Action<IState> onStateCompleted)
        {
            _failUI.Activate();
            _counter.Count(_uiShowDuration, null, OnUIShown);
        }

        private void OnUIShown()
        {
            _failUI.Deactivate();
            _eventBus.Publish(GamePlayEvent.LevelFail,null);
        }
    }
}