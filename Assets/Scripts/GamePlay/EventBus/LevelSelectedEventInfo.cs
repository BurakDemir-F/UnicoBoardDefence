using General;

namespace GamePlay
{
    public struct LevelSelectedEventInfo : IEventInfo
    {
        public int SelectedLevel { get; private set; }
        public LevelProperties LevelProperties { get; private set; }

        public LevelSelectedEventInfo(int selectedLevel, LevelProperties levelProperties)
        {
            SelectedLevel = selectedLevel;
            LevelProperties = levelProperties;
        }
    }
}