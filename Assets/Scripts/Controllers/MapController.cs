using System.Collections.Generic;
using GamePlay;
using GamePlay.Areas;
using GamePlay.Areas.Trigger;
using GamePlay.Defenders;
using GamePlay.Enemies;
using GamePlay.EventBus;
using GamePlay.EventBus.Info;
using GamePlay.Map;
using GamePlay.Map.MapGrid;
using General;
using General.Pool.System;
using UnityEngine;

namespace Controllers
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private LevelDataProvider _levelDataProvider;
        [SerializeField] private EnemyController _enemyController;
        [SerializeField] private ItemActionsEventBus _itemActions;
        private IMap _map;
        private IAreaController _areaController;
        private IDefenderController _defenderController;

        private bool _isInitialized;
        
        private void Awake()
        {
            _areaController = GetComponent<IAreaController>();
            _defenderController = GetComponent<IDefenderController>();
            _enemyController.EnemyDeath += OnEnemyDeath;
            _itemActions.Subscribe(ItemActions.DefenceItemSelected,OnDefenceItemSelected);
        }

        public void Initialize(IMap map,IPoolCollection poolCollection)
        {
            _isInitialized = true;
            _map = map;
            _map.AreaTriggerEntered += OnAreaTriggerEnter;
            _map.AreaTriggerExited += OnAreaTriggerExit;
            _areaController.Initialize(map);
            _defenderController.Initialize(poolCollection,_levelDataProvider.GetDefenderData());
        }

        private void OnDestroy()
        {
            _enemyController.EnemyDeath -= OnEnemyDeath;
            _itemActions.UnSubscribe(ItemActions.DefenceItemSelected,OnDefenceItemSelected);
            
            if(!_isInitialized)
                return;
            
            _map.AreaTriggerEntered -= OnAreaTriggerEnter;
            _map.AreaTriggerExited -= OnAreaTriggerExit;
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
                var enemy = info.TriggerItem.TriggerObject.GetComponent<IEnemy>();
                HandleEnemyEnter(enemy,gameArea);
                return;
            }

            if (IsEnemy(info))
            {
                var enemy = info.TriggerItem.TriggerObject.GetComponent<IEnemy>();
                _defenderController.UnTrackEnemy(enemy);
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
        
        private void HandleEnemyEnter(IEnemy enemy, GameArea area)
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
            _defenderController.UnTrackEnemy(enemy);
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
                    _defenderController.HandleEnemyAreaEnter(gameArea,enemy);
                }
            }
        }
    }
}