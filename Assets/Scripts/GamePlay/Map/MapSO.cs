using System.Collections.Generic;
using GamePlay.Map.MapGrid;
using General.GridSystem;
using UnityEngine;

namespace GamePlay.Map
{
    [CreateAssetMenu(menuName = "ScriptableData/Map", fileName = "Map", order = 0)]
    public class MapSO : ScriptableObject,IMap
    {
        public IGrid Grid { get; private set; }
        public List<SpawnArea> SpawnAreas { get; private set; }
        public List<EmptyArea> EmptyAreas { get; private set; }
        public List<DefenderArea> DefenderAreas { get; private set; }
        public List<PlayerLooseArea> PlayerLooseAreas { get; private set; }

        public void InitializeMap(IGrid grid,
            List<SpawnArea> spawnAreas,
            List<EmptyArea> emptyAreas,
            List<DefenderArea> defenderAreas,
            List<PlayerLooseArea> playerLooseAreas)
        {
            Grid = grid;
            SpawnAreas = spawnAreas;
            EmptyAreas = emptyAreas;
            DefenderAreas = defenderAreas;
            PlayerLooseAreas = playerLooseAreas;
        }
    }
}