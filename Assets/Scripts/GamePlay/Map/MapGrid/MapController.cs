using System;
using Defenders;
using GamePlay.Areas;
using General.Pool.System;
using UnityEngine;

namespace GamePlay.Map.MapGrid
{
    public class MapController : MonoBehaviour
    {
        private IMap _map;
        private IAreaController _areaController;
        private IDefenderController _defenderController;

        private void Awake()
        {
            _areaController = GetComponent<IAreaController>();
            _defenderController = GetComponent<IDefenderController>();
        }

        public void Initialize(IMap map,IPoolCollection poolCollection)
        {
            _map.AreaTriggerEntered += OnAreaTriggerEnter;
            map.AreaTriggerExited += OnAreaTriggerExit;
            _areaController.Initialize(map);
            _defenderController.Initialize(poolCollection);
        }

        private void OnDestroy()
        {
            _map.AreaTriggerEntered -= OnAreaTriggerEnter;
            _map.AreaTriggerExited -= OnAreaTriggerExit;
        }

        private void OnAreaTriggerEnter(ITriggerInfo info)
        {
            var triggerItem = info.TriggerItem;
            var triggerType = triggerItem.TriggerItemType;
            var triggerArea = info.TriggeredArea;
            if (triggerType == TriggerItemType.Enemy && triggerArea is GameArea)
            {
                HandleEnemyEnter(triggerItem.TriggerObject.transform,(GameArea)triggerArea);
            }
        }

        private void OnAreaTriggerExit(ITriggerInfo info)
        {
            
        }

        private void HandleDefenderEnter(DefenceItemBase defenceItem, GameArea area)
        {
            
        }
        
        private void HandleEnemyEnter(Transform enemy, GameArea area)
        {
            _defenderController.HandleEnemyAreaEnter(area,enemy);
        }

        private void PlaceDefender(DefenderArea area, DefenceItemBase defenceItem)
        {
            _areaController.Place(area,defenceItem);
            // _defenderController.AddDefender(defender);
            var defenderData = defenceItem.DefenderData;
            var inRangeAreas = _areaController.GetInRangeAreas(area, defenderData);
            _defenderController.AddAttackableAreas(defenceItem, inRangeAreas);
        }
    }
}