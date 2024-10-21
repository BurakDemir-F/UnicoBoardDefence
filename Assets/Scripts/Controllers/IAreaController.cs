using System.Collections.Generic;
using GamePlay.Areas;
using GamePlay.Defenders;
using GamePlay.Map.MapGrid;

namespace Controllers
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