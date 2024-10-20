using GamePlay.Map.MapGrid;
using General;

namespace GamePlay.GamePlayStates
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