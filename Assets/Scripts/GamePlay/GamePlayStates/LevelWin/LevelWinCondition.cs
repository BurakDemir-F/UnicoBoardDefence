using GamePlay.EventBus;
using General;
using UnityEngine;

namespace GamePlay.GamePlayStates.LevelWin
{
    public class LevelWinCondition : GameStateCondition
    {
        [SerializeField] private ItemActionsEventBus _eventBus;
        public override void Construct()
        {
            base.Construct();
            _eventBus.Subscribe(ItemActions.AllEnemiesDead,OnAllEnemiesDead);
        }

        public override void Destruct()
        {
            base.Destruct();
            _eventBus.UnSubscribe(ItemActions.AllEnemiesDead,OnAllEnemiesDead);
        }
    
        private void OnAllEnemiesDead(IEventInfo eventInfo)
        {
            InvokeConditionMetEvent();
        } 
    }
}