using System;
using System.Collections.Generic;
using System.Linq;
using GamePlay.Areas;
using General;
using General.GridSystem;
using General.Pool.System;
using UnityEngine;
using Grid = General.GridSystem.Grid;

namespace GamePlay.Map.MapGrid
{
    public class MapGridCreator : MonoBehaviour,IMapGridCreator
    {
        [SerializeField] private Transform _mapRoot;
        [SerializeField] private MapSO _map;
        [SerializeField] private MasterPool _poolCollection;

        private Grid _grid;
        private GridPositionProvider _positionProvider;
        private IMapAnimator _mapAnimator;
        
        private Action<IMap> _onMapCreated;
        public void Awake()
        {
            _mapAnimator = GetComponent<IMapAnimator>();
        }

        public void CreateMapGrid(IMapData mapData,Action<IMap> onMapCreated)
        {
            _onMapCreated = onMapCreated;
            _positionProvider =
                new BottomLeftGridPositionProvider(_mapRoot, mapData.CellSize, mapData.Padding);
            CreateAreas(mapData);
        }

        private void CreateAreas(IMapData mapData)
        {
            var emptyAreaPoolKey = mapData.EmptyAreaKey;
            var defenderAreaPoolKey = mapData.DefenderAreaKey;
            var spawnAreaPoolKey = mapData.SpawnAreaKey;
            var playerLooseAreaPoolKey = mapData.PlayerLooseAreaKey;
            var mapXDimension = mapData.DefenderDimension.XLength;
            var mapYDimension = mapData.DefenderDimension.YLength + mapData.EmptyDimension.YLength + 2;//(2)spawn area and player loose area add height to the map
            
            var areas = new List<AreaBase>(mapXDimension * mapYDimension);
            var defenderAreas = new List<DefenderArea>(mapData.DefenderDimension.Size);
            var emptyAreas = new List<NonDefenderArea>(mapData.EmptyDimension.Size);
            var looseAreas = new List<PlayerLooseArea>(mapXDimension);
            var spawnAreas = new List<SpawnArea>(mapXDimension);
            
            for (int i = 0; i < mapXDimension; i++)
            {
                var area = _poolCollection.Get<SpawnArea>(spawnAreaPoolKey);
                spawnAreas.Add(area);
            }
            
            for (var i = 0; i < mapData.EmptyDimension.Size; i++)
            {
                var area = _poolCollection.Get<NonDefenderArea>(emptyAreaPoolKey);
                emptyAreas.Add(area);
            }
            
            for (var i = 0; i < mapData.DefenderDimension.Size; i++)
            {
                var area = _poolCollection.Get<DefenderArea>(defenderAreaPoolKey);
                defenderAreas.Add(area);
            }
            
            for (int i = 0; i < mapXDimension; i++)
            {
                var area = _poolCollection.Get<PlayerLooseArea>(playerLooseAreaPoolKey);
                looseAreas.Add(area);
            }
            
            areas.AddRange(looseAreas);
            areas.AddRange(defenderAreas);
            areas.AddRange(emptyAreas);
            areas.AddRange(spawnAreas);
            var cells = areas.Select(area => area as IGridCell).ToList();
            _grid = new Grid(cells, mapXDimension, mapYDimension);
            _map.InitializeMap(_grid,spawnAreas,emptyAreas,defenderAreas,looseAreas);
            _mapAnimator.PlayPlacementAnimation(areas,_positionProvider,mapData.GridCreateInterval,OnMapAnimationsCompleted);
        }

        private void OnMapAnimationsCompleted()
        {
            _onMapCreated?.Invoke(_map);
        }
    }

    public interface IMapGridCreator
    {
        void CreateMapGrid(IMapData mapData, Action<IMap> onMapCreated);
    }
}