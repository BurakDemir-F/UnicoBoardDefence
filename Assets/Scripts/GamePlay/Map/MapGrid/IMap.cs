using System;
using System.Collections.Generic;
using GamePlay.Areas;
using General.GridSystem;

namespace GamePlay.Map.MapGrid
{
    public interface IMap
    {
        IGrid Grid { get; }
        List<SpawnArea> SpawnAreas { get; }
        List<NonDefenderArea> EmptyAreas { get; }
        List<DefenderArea> DefenderAreas { get; }
        List<PlayerLooseArea> PlayerLooseAreas { get; }

        event Action MapInitialized;
        event Action<ITriggerInfo> AreaTriggerEntered;
        event Action<ITriggerInfo> AreaTriggerExited;
        
    }
}