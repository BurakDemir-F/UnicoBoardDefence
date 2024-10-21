using UnityEngine;

namespace GamePlay.Map
{
    public class LevelDataProvider : MonoBehaviour,ILevelDataProvider
    {
        [SerializeField] private LevelSO _levelData;
        [SerializeField] private GameItemsSO _gameItems;
        public ILevelData GetLevel()
        {
            return _levelData.LevelData;
        }

        public IMapData GetMapData()
        {
            return _levelData.MapData;
        }

        public IEnemyProperties GetEnemyData()
        {
            return _gameItems;
        }

        public IDefenderProperties GetDefenderData()
        {
            return _gameItems;
        }
    }
}