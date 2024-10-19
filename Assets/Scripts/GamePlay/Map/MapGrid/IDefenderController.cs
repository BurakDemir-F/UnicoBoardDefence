using System.Collections.Generic;
using Defenders;
using GamePlay.Areas;
using General.Pool.System;
using UnityEngine;

namespace GamePlay.Map.MapGrid
{
    public interface IDefenderController
    {
        void AddAttackableAreas(DefenceItemBase defenceItem, HashSet<GameArea> inRangeAreas);
        void HandleEnemyAreaEnter(GameArea area, Transform enemy);
        void Initialize(IPoolCollection poolCollection);
        DefenceItemBase CreateDefender(DefenderData data);
    }
}