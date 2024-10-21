using System.Collections.Generic;
using GamePlay;
using GamePlay.Areas;
using GamePlay.Defenders;
using GamePlay.Enemies;
using GamePlay.EventBus;
using GamePlay.EventBus.Info;
using GamePlay.Map.MapGrid;
using General;
using General.Pool.System;
using UnityEngine;

namespace Controllers
{
    public class DefenderController : MonoBehaviour, IDefenderController
    {
        [SerializeField] private GamePlayEventBus _gamePlayEventBus;
        [SerializeField] private ItemActionsEventBus _itemActionsEventBus;
        private Dictionary<DefenceItemBase, HashSet<GameArea>> _defenderInRangeAreaDict = new ();
        private HashSet<DefenceItemBase> _defenders = new();
        private IPoolCollection _poolCollection;
        private IDefenderProperties _defenderItemDatas;
        private Dictionary<DefenderType, int> _remainingDefenceItems = new();
        private LevelProperties _levelProperties;

        public void Initialize(IPoolCollection poolCollection,IDefenderProperties defenderItemDatas)
        {
            _poolCollection = poolCollection;
            _defenderItemDatas = defenderItemDatas;
        }

        private void Awake()
        {
            _gamePlayEventBus.Subscribe(GamePlayEvent.LevelSelected,OnLevelSelected);
            _gamePlayEventBus.Subscribe(GamePlayEvent.LevelWin,OnLevelEnd);
            _gamePlayEventBus.Subscribe(GamePlayEvent.LevelFail,OnLevelEnd);
        }

        private void OnDestroy()
        {
            _gamePlayEventBus.UnSubscribe(GamePlayEvent.LevelSelected,OnLevelSelected);
            _gamePlayEventBus.UnSubscribe(GamePlayEvent.LevelWin,OnLevelEnd);
            _gamePlayEventBus.UnSubscribe(GamePlayEvent.LevelFail,OnLevelEnd);
        }

        private void OnLevelSelected(IEventInfo info)
        {
            var levelInfo = (LevelSelectedEventInfo)info;
            _levelProperties = levelInfo.LevelProperties;
            _remainingDefenceItems.Clear();
            
            foreach (var (type, count) in _levelProperties.Defenders)
                _remainingDefenceItems.Add(type,count);
            
            InvokeRemainingItems();
        }

        public void HandleEnemyAreaEnter(GameArea area,IEnemy enemy)
        {
            TryUnTrackEnemy(area,enemy);
            TryTrackEnemy(area,enemy);
        }
        
        public DefenceItemBase CreateDefender(DefenderType type)
        {
            var data = _defenderItemDatas.DefenderItemProperties[type];
            var defender = _poolCollection.Get<DefenceItemBase>(data.PoolKey);
            defender.transform.SetParent(transform);
            defender.Initialize(data,_poolCollection);
            _defenders.Add(defender);
            UpdateRemainingDefenceItems(type);
            return defender;
        }

        private void UpdateRemainingDefenceItems(DefenderType defenderType)
        {
            if (_remainingDefenceItems[defenderType] > 0)
            {
                _remainingDefenceItems[defenderType]--;
                InvokeRemainingItems();
            }
        }

        private void InvokeRemainingItems()
        {
            _itemActionsEventBus.Publish(ItemActions.RemainingDefenceItemsChanged,
                new RemainingItemsInfo(_remainingDefenceItems));
        }

        public void AddAttackableAreas(DefenceItemBase defenceItem, HashSet<GameArea> inRangeAreas)
        {
            _defenderInRangeAreaDict.Add(defenceItem,inRangeAreas);
        }

        private void TryUnTrackEnemy(GameArea area,IEnemy enemy)
        {
            foreach (var (defender, inRangeAres) in _defenderInRangeAreaDict)
            {
                if (!inRangeAres.Contains(area))
                {
                    defender.RemoveTarget(enemy);
                }
            }
        }

        private void TryTrackEnemy(GameArea area, IEnemy enemy)
        {
            var isInAttackRange = IsInAttackRange(area);
            if (isInAttackRange)
            {
                var hasRangeDefenders = GetDefendersHasRange(area);
                foreach (var hasRangeDefender in hasRangeDefenders)
                {
                    hasRangeDefender.AddTarget(enemy);
                }
            }
        }
        
        public void UnTrackEnemy(IEnemy enemyTransform)
        {
            foreach (var defenceItem in _defenders)
            {
                if (defenceItem.IsTrackingEnemy(enemyTransform))
                {
                    defenceItem.RemoveTarget(enemyTransform);
                }
            }
        }

        public void UpdateVisibility(DefenceItemBase defenceItem, bool shouldBeTransparent)
        {
            if(shouldBeTransparent)
                defenceItem.MakeTransparent();
            else
                defenceItem.MakeOpaque();
        }

        private bool IsInAttackRange(GameArea area)
        {
            foreach (var (defenderArea, inRangeAreas) in _defenderInRangeAreaDict)
            {
                if (inRangeAreas.Contains(area))
                    return true;
            }

            return false;
        }

        private List<DefenceItemBase> GetDefendersHasRange(GameArea area)
        {
            var defenders = new List<DefenceItemBase>();
            foreach (var (defender, inRangeAreas) in _defenderInRangeAreaDict)
            {
                if (inRangeAreas.Contains(area))
                {
                    defenders.Add(defender);
                }
            }

            return defenders;
        }
        
        private void OnLevelEnd(IEventInfo info)
        {
            foreach (var defenceItemBase in _defenders)
            {
                defenceItemBase.ReturnToPool();
            }
            
            _defenders.Clear();
            _defenderInRangeAreaDict.Clear();
        }
    }
}