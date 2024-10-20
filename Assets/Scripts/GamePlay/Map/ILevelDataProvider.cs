namespace GamePlay.Map
{
    public interface ILevelDataProvider
    {
        ILevelData GetLevel();
        IMapData GetMapData();
    }
}