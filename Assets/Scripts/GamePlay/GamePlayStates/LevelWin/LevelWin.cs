using System;
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

        public override void Construct()
        {
            base.Construct();
            _counter = GetComponent<ICounter>();
        }

        public override void PlayState(Action<IState> onStateCompleted)
        {
            _winUI.Activate();
            _counter.Count(_uiShowDuration, null, OnUIShown);
        }

        private void OnUIShown()
        {
            _winUI.Deactivate();
            _eventBus.Publish(GamePlayEvent.LevelWin,null);
        }
    }
}