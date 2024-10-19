using Defenders;
using GamePlay.Areas;
using General.Pool.System;
using UnityEngine;

namespace GamePlay.Map.MapGrid
{
    public class MapController : MonoBehaviour
    {
        [SerializeField]private LevelDataProvider _levelDataProvider;
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
            _map = map;
            _map.AreaTriggerEntered += OnAreaTriggerEnter;
            _map.AreaTriggerExited += OnAreaTriggerExit;
            _areaController.Initialize(map);
            _defenderController.Initialize(poolCollection,_levelDataProvider.GetDefenderData());
        }

        private void OnDestroy()
        {
            _map.AreaTriggerEntered -= OnAreaTriggerEnter;
            _map.AreaTriggerExited -= OnAreaTriggerExit;
        }

        private void OnAreaTriggerEnter(ITriggerInfo info)
        {
            if(IsGameArea(info))
            {
                HandleEnemyEnter(info.TriggerItem.TriggerObject.transform,(GameArea)info.TriggeredArea);
            }
        }

        private void OnAreaTriggerExit(ITriggerInfo info)
        {
            if(IsGameArea(info))
            {
                HandleEnemyExit(info.TriggerItem.TriggerObject.transform,(GameArea)info.TriggeredArea);
            }
        }
        
        
        private void HandleEnemyEnter(Transform enemy, GameArea area)
        {
            _defenderController.HandleEnemyAreaEnter(area,enemy);
            TryUpdateVisibility(area, false);
        }

        private void HandleEnemyExit(Transform enemy, GameArea area)
        {
            TryUpdateVisibility(area,true);
        }

        private void TryUpdateVisibility(GameArea area, bool isExit)
        {
            if (area.AreaType == AreaType.DefenderArea)
            {
                var defenderArea = (DefenderArea)area;
                if (defenderArea.IsPlaced)
                {
                    var shouldBeTransparent = !isExit;
                    _defenderController.UpdateVisibility(defenderArea.PlacedItem.DefenceItem,shouldBeTransparent);
                }
            }
        }

        private bool IsGameArea(ITriggerInfo info)
        {
            var triggerItem = info.TriggerItem;
            var triggerType = triggerItem.TriggerItemType;
            var triggerArea = info.TriggeredArea;
            return triggerType == TriggerItemType.Enemy &&
                   triggerArea.AreaType is AreaType.DefenderArea or AreaType.NonDefenderArea;
        }

        private void PlaceDefender(DefenderArea area, DefenceItemBase defenceItem)
        {
            _areaController.Place(area,defenceItem);
            var defenderData = defenceItem.DefenderData;
            var inRangeAreas = _areaController.GetInRangeAreas(area, defenderData);
            _areaController.IndicateInRangeAreas(inRangeAreas);
            _defenderController.AddAttackableAreas(defenceItem, inRangeAreas);
        }

        [ContextMenu("PlaceTest")]
        private void PlaceTest()
        {
            var defenderArea1 = _map.DefenderAreas[6];
            var defenceItem1 = _defenderController.CreateDefender(DefenderType.Damaging);
            PlaceDefender(defenderArea1,defenceItem1);
            
            
            var defenderArea2 = _map.DefenderAreas[2];
            var defenceItem2 = _defenderController.CreateDefender(DefenderType.Average);
            PlaceDefender(defenderArea2,defenceItem2);
            
            
            var defenderArea = _map.DefenderAreas[11];
            var defenceItem = _defenderController.CreateDefender(DefenderType.Ranger);
            PlaceDefender(defenderArea,defenceItem);
        }
    }
}