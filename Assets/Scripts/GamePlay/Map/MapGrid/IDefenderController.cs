using System.Collections.Generic;
using DefaultNamespace;
using Defenders;
using GamePlay.Areas;
using General.Pool.System;
using UnityEngine;

namespace GamePlay.Map.MapGrid
{
    public interface IDefenderController
    {
        void Initialize(IPoolCollection poolCollection,IDefenderProperties defenderItemDatas);
        void AddAttackableAreas(DefenceItemBase defenceItem, HashSet<GameArea> inRangeAreas);
        void HandleEnemyAreaEnter(GameArea area, Transform enemy);
        DefenceItemBase CreateDefender(DefenderType data);
        void UpdateVisibility(DefenceItemBase defenceItem,bool shouldBeTransparent);
    }
}