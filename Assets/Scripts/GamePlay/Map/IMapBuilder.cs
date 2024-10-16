using System;

namespace GamePlay.Map
{
    public interface IMapBuilder
    {
        void BuildMap(IMapData levelData, Action onMapBuilt);
    }
}