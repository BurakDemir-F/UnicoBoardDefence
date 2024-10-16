using UnityEngine;

namespace GamePlay.Map
{
    public interface IMapData
    {
        Vector2Int PlaceableSize { get; }
        Vector2Int NonPlaceableSize { get; }
        string PlaceablePoolKey { get; }
        string NonPlaceablePoolKey { get; }
        float GridCreateInterval { get; }
    }
}