using System.Collections.Generic;
using GamePlay.Defenders;
using General;

namespace GamePlay.EventBus.Info
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