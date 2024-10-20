using System.Collections.Generic;
using Defenders;
using General;

namespace GamePlay.Map.MapGrid
{
    public struct RemainingItemsInfo : IEventInfo
    {
        public Dictionary<DefenderType, int> RemainingDefenceItems { get; private set; }

        public RemainingItemsInfo(Dictionary<DefenderType, int> remainingDefenceItems)
        {
            RemainingDefenceItems = remainingDefenceItems;
        }
    }
}