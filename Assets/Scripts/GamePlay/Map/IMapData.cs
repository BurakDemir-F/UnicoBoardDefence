using General;

namespace GamePlay.Map
{
    public interface  IMapData
    {
        public Dimension2D DefenderDimension { get; }
        public Dimension2D EmptyDimension { get; }
        public float CellSize { get; }
        public float Padding { get; }
        public string DefenderAreaKey { get; }
        public string EmptyAreaKey { get; }
        public string SpawnAreaKey { get; }
        public string PlayerLooseAreaKey { get; }
        public float GridCreateInterval { get; }
    }
}