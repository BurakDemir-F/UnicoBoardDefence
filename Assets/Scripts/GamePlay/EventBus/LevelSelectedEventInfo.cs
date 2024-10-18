using General;

namespace GamePlay
{
    public struct LevelSelectedEventInfo : IEventInfo
    {
        public int SelectedLevel { get; private set; }

        public LevelSelectedEventInfo(int selectedLevel)
        {
            SelectedLevel = selectedLevel;
        }
    }
}