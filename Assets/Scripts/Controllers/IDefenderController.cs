using System.Collections.Generic;
using GamePlay.Areas;
using GamePlay.Defenders;
using GamePlay.Enemies;
using General.Pool.System;

namespace Controllers
{
    public interface IDefenderController
    {
        void Initialize(IPoolCollection poolCollection,IDefenderProperties defenderItemDatas);
        void AddAttackableAreas(DefenceItemBase defenceItem, HashSet<GameArea> inRangeAreas);
        void HandleEnemyAreaEnter(GameArea area, IEnemy enemy);
        DefenceItemBase CreateDefender(DefenderType data);
        void UpdateVisibility(DefenceItemBase defenceItem,bool shouldBeTransparent);
        void UnTrackEnemy(IEnemy enemyTransform);
    }
}