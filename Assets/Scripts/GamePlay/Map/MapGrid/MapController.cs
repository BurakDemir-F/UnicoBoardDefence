using System.Collections.Generic;
using Defenders;
using Defenders.UI;
using GamePlay.Areas;
using GamePlay.Enemies;
using GamePlay.Spawner;
using General;
using General.Pool.System;
using UnityEngine;

namespace GamePlay.Map.MapGrid
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private LevelDataProvider _levelDataProvider;
        [SerializeField] private EnemyController _enemyController;
        [SerializeField] private ItemActionsEventBus _itemActions;
        private IMap _map;
        private IAreaController _areaController;
        private IDefenderController _defenderController;
        
        private void Awake()
        {
            _areaController = GetComponent<IAreaController>();
            _defenderController = GetComponent<IDefenderController>();
            _enemyController.EnemyDeath += OnEnemyDeath;
            _itemActions.Subscribe(ItemActions.DefenceItemSelected,OnDefenceItemSelected);
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
            _enemyController.EnemyDeath -= OnEnemyDeath;
            _itemActions.UnSubscribe(ItemActions.DefenceItemSelected,OnDefenceItemSelected);
        }

        private void OnDefenceItemSelected(IEventInfo eventInfo)
        {
            var defenderSelectInfo = (DefenceItemSelectedEventInfo)eventInfo;
            var defenderType = defenderSelectInfo.DefenderType;
            var defenderArea = defenderSelectInfo.DefenderArea;
            var isPlaceable = _areaController.IsPlaceable(defenderArea);
            if (isPlaceable)
            {
                var defender = _defenderController.CreateDefender(defenderType);
                PlaceDefender(defenderArea,defender);
            }
        }
        
        private void OnAreaTriggerEnter(ITriggerInfo info)
        {
            if(IsEnemyOnGameArea(info))
            {
                var gameArea = (GameArea)info.TriggeredArea;
                gameArea.DrawGizmo();
                HandleEnemyEnter(info.TriggerItem.TriggerObject.transform,gameArea);
                return;
            }

            if (IsEnemy(info))
            {
                _defenderController.UnTrackEnemy(info.TriggerItem.TriggerObject.transform);
            }
        }

        private void OnAreaTriggerExit(ITriggerInfo info)
        {
            if(IsEnemyOnGameArea(info))
            {
                var gameArea = (GameArea)info.TriggeredArea;
                gameArea.CloseGizmo();
                HandleEnemyExit(info.TriggerItem.TriggerObject.transform,gameArea);
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
        
        private void OnEnemyDeath(EnemyBase enemy)
        {
            _defenderController.UnTrackEnemy(enemy.transform);
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

        private bool IsEnemyOnGameArea(ITriggerInfo info)
        {
            var triggerArea = info.TriggeredArea;
            return IsEnemy(info) &&
                   triggerArea.AreaType is AreaType.DefenderArea or AreaType.NonDefenderArea;
        }

        private bool IsEnemy(ITriggerInfo info)
        {
            return info.TriggerItem.TriggerItemType == TriggerItemType.Enemy;
        }
        
        private void PlaceDefender(DefenderArea area, DefenceItemBase defenceItem)
        {
            _areaController.Place(area,defenceItem);
            var defenderData = defenceItem.DefenderData;
            var inRangeAreas = _areaController.GetInRangeAreas(area, defenderData);
            _areaController.IndicateInRangeAreas(inRangeAreas);
            _defenderController.AddAttackableAreas(defenceItem, inRangeAreas);
            CheckForEnemiesAfterPlacing(area,inRangeAreas);
        }

        private void CheckForEnemiesAfterPlacing(DefenderArea placedArea, HashSet<GameArea> inRangeAreas)
        {
            foreach (var (enemy, area) in _enemyController.EnemyAreaDictionary)
            {
                if(area.AreaType != AreaType.DefenderArea ||area.AreaType != AreaType.NonDefenderArea)
                    continue;
                
                if (area == placedArea)
                    TryUpdateVisibility(placedArea,false);

                var gameArea = (GameArea)area;
                if (inRangeAreas.Contains(gameArea))
                {
                    _defenderController.HandleEnemyAreaEnter(gameArea,enemy.transform);
                }
            }
        }
    }
}