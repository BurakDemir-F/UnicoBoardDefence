using System;
using General.StateMachines;
using UnityEngine;

namespace GamePlay.TutorialStates
{
    public class TutorialState : GameState
    {
        protected ITutorialData _tutorialData;
        protected IGridActionsProvider _gridActionsProvider;
        protected ITutorialUI _tutorialUI;
        public override void PlayState(Action<IState> onStateCompleted)
        {
            
        }
    }
    
    public interface IGridActionsProvider
    {
        void SelectBuildArea(Vector2Int area);
        event Action<Vector2Int> BuildAreaSelected;
        event Action<Vector2Int> BuildAreaEmpty;
    }

    public interface ITutorialUI
    {
        void ActivateDefenderAreaIndicator();
        void DeactivateBuildAreaIndicator();
    }
}