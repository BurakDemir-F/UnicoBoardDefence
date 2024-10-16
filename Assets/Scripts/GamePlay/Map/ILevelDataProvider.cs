using GamePlay.TutorialStates;

namespace GamePlay.Map
{
    public interface ILevelDataProvider
    {
        ILevelData GetLevel();
        IMapData GetMapData();
        ITutorialData GetTutorialData();
    }
}