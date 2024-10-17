using System;
using GamePlay.Map.MapGrid;
using General;

namespace GamePlay.Map
{
    public interface IMapBuilder : IConstructionProvider
    {
        void BuildMap(IMapData mapData, Action<IMap> onMapBuilt);
    }
}