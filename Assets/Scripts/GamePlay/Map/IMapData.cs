using General;

namespace GamePlay.Map
{
    public interface IMapData
    {
        Dimension2D DefenderDimension { get; }
        Dimension2D EmptyDimension { get; }
        float CellSize { get; }
        float Padding { get; }
        string DefenderAreaKey { get; }
        string EmptyAreaKey { get; }
        string SpawnAreaKey { get; }
        string PlayerLooseAreaKey { get; }
        
        float GridCreateInterval { get; }
    }
}