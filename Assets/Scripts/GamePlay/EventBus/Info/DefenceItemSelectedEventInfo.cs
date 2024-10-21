using GamePlay.Areas;
using GamePlay.Defenders;
using General;

namespace GamePlay.EventBus.Info
{
    public struct DefenceItemSelectedEventInfo : IEventInfo
    {
        public DefenderType DefenderType { get; private set; }
        public DefenderArea DefenderArea { get; private set; }

        public DefenceItemSelectedEventInfo(DefenderType defenderType, DefenderArea area)
        {
            DefenderType = defenderType;
            DefenderArea = area;
        }
    }
}