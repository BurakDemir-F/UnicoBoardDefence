using System;
using Controllers;
using GamePlay.EventBus;
using General;
using General.Counter;
using General.StateMachines;
using UnityEngine;

namespace GamePlay.GamePlayStates.LevelWin
{
    public class LevelWin : GameState
    {
        [SerializeField] private LevelEndUI _winUI;
        [SerializeField] private float _uiShowDuration = 2f;
        [SerializeField] private InteractionController _interactionController;
        [SerializeField] private AreaController _areaController;
        private Action<IState> _onStateCompleted;

        public override void PlayState(Action<IState> onStateCompleted)
        {
            _winUI.Activate();
            _eventBus.Publish(GamePlayEvent.LevelWin,null);
            _interactionController.DisableInteraction();
            _areaController.AllAreasPicked += OnMapRemoved;
            _onStateCompleted = onStateCompleted;
        }

        private void OnMapRemoved()
        {
            _areaController.AllAreasPicked -= OnMapRemoved;
            FinishLevel();
        }

        private void FinishLevel()
        {
            _interactionController.EnableInteraction();
            _winUI.Deactivate();
            _onStateCompleted?.Invoke(this);
            _eventBus.Publish(GamePlayEvent.LevelEnd,null);
        }
    }
}