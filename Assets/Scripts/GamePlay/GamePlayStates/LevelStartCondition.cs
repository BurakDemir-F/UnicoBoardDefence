using General;
using UnityEngine;
using Utilities;

namespace GamePlay.GamePlayStates
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
            "On Level started catched from level start condition.".PrintColored(Color.white);
            InvokeConditionMetEvent();
        }
    }
}