using GamePlay.EventBus;
using General;
using UnityEngine;

namespace GamePlay.GamePlayStates.LevelStart
{
    public class LevelStartCondition : GameStateCondition
    {
        [SerializeField] private GamePlayEventBus _eventBus;

        public override void Construct()
        {
            base.Construct();
            _eventBus.Subscribe(GamePlayEvent.LevelStarted,OnLevelStarted);     
        }

        public override void Destruct()
        {
            base.Destruct();
            _eventBus.UnSubscribe(GamePlayEvent.LevelStarted,OnLevelStarted);
        }

        private void OnLevelStarted(IEventInfo eventInfo)
        {
            InvokeConditionMetEvent();
        }
    }
}