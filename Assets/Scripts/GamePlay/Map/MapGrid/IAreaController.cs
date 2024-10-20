using System.Collections.Generic;
using Defenders;
using GamePlay.Areas;

namespace GamePlay.Map.MapGrid
{
    public interface IAreaController
    {
        void Initialize(IMap map);
        void Place(DefenderArea area,DefenceItemBase defenceItem);
        HashSet<GameArea> GetInRangeAreas(DefenderArea placedArea,DefenderData defenderData);
        void IndicateInRangeAreas(HashSet<GameArea> inRangeAreas);
        bool IsPlaceable(DefenderArea area);
    }
}