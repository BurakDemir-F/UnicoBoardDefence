using System;
using System.Collections.Generic;
using GamePlay.Areas;
using General.GridSystem;
using UnityEngine;

namespace GamePlay.Map.MapGrid
{
    public interface IMap : IEnumerable<AreaBase>
    {
        IGrid Grid { get; }
        List<SpawnArea> SpawnAreas { get; }
        List<NonDefenderArea> NonDefenderAreas { get; }
        List<DefenderArea> DefenderAreas { get; }
        List<PlayerLooseArea> PlayerLooseAreas { get; }
        float GetOneAreaLength();
        bool TryGetVisiblePosition(DefenderArea area, out Vector3 visiblePos);
        event Action MapInitialized;
        event Action<ITriggerInfo> AreaTriggerEntered;
        event Action<ITriggerInfo> AreaTriggerExited;
        
    }
}