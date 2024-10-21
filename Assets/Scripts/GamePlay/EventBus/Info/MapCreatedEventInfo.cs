using GamePlay.Map.MapGrid;
using General;

namespace GamePlay.EventBus.Info
{
    public struct MapCreatedEventInfo : IEventInfo
    {
        public IMap Map { get; private set; }

        public MapCreatedEventInfo(IMap map)
        {
            Map = map;
        }
    }
}