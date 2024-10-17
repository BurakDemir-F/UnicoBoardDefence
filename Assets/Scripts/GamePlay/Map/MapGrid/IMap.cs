using System.Collections.Generic;
using General.GridSystem;
using UnityEngine;

namespace GamePlay.Map.MapGrid
{
    public interface IMap
    {
        IGrid Grid { get; }
        List<SpawnArea> SpawnAreas { get; }
        List<EmptyArea> EmptyAreas { get; }
        List<DefenderArea> DefenderAreas { get; }
        List<PlayerLooseArea> PlayerLooseAreas { get; }
    }
}