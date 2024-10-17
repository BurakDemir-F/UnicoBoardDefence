using System;
using GamePlay.Map.MapGrid;
using UnityEngine;

namespace GamePlay.Map
{
    public class MapBuilder :MonoBehaviour, IMapBuilder
    {
        private IMapGridCreator _mapGridCreator;

        public void Construct()
        {
            _mapGridCreator = GetComponent<IMapGridCreator>();
        }

        public void Destruct()
        {
            
        }

        public void BuildMap(IMapData mapData,Action<IMap> onMapBuilt)
        {
            _mapGridCreator.CreateMapGrid(mapData,onMapBuilt);
        }
    }
}