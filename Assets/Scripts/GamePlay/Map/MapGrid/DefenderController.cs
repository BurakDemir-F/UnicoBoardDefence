using System.Collections.Generic;
using DefaultNamespace;
using Defenders;
using GamePlay.Areas;
using General.Pool.System;
using UnityEngine;

namespace GamePlay.Map.MapGrid
{
    public class DefenderController : MonoBehaviour, IDefenderController
    {
        private Dictionary<DefenceItemBase, HashSet<GameArea>> _defenderInRangeAreaDict = new ();
        private HashSet<DefenceItemBase> _defenders = new();
        private IPoolCollection _poolCollection;
        private IDefenderProperties _defenderItemDatas;
        public void Initialize(IPoolCollection poolCollection,IDefenderProperties defenderItemDatas)
        {
            _poolCollection = poolCollection;
            _defenderItemDatas = defenderItemDatas;
        }

        public void HandleEnemyAreaEnter(GameArea area,Transform enemy)
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
            return defender;
        }

        public void AddAttackableAreas(DefenceItemBase defenceItem, HashSet<GameArea> inRangeAreas)
        {
            _defenderInRangeAreaDict.Add(defenceItem,inRangeAreas);
        }

        private void TryUnTrackEnemy(GameArea area,Transform enemyTransform)
        {
            foreach (var (defender, inRangeAres) in _defenderInRangeAreaDict)
            {
                if (!inRangeAres.Contains(area))
                {
                    defender.RemoveTarget(enemyTransform);
                }
            }
        }

        private void TryTrackEnemy(GameArea area, Transform enemy)
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
    }
}