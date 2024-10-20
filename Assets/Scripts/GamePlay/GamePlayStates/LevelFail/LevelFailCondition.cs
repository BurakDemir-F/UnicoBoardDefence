using GamePlay.Areas;
using GamePlay.Map;
using UnityEngine;

namespace GamePlay.GamePlayStates.LevelFail
{
    public class LevelFailCondition : GameStateCondition
    {
        [SerializeField] private MapSO _map;
        public override void Construct()
        {
            base.Construct();
            _map.AreaTriggerEntered += OnAreaTriggerEnter;
        }

        public override void Destruct()
        {
            base.Destruct();
            _map.AreaTriggerEntered -= OnAreaTriggerEnter;
        }

        private void OnAreaTriggerEnter(ITriggerInfo triggerInfo)
        {
            var triggerType = triggerInfo.TriggerItem.TriggerItemType;
            if(triggerType != TriggerItemType.Enemy)
                return;

            var area = triggerInfo.TriggeredArea;
            if(area.AreaType == AreaType.LooseArea)
                InvokeConditionMetEvent();
        }
    }
}