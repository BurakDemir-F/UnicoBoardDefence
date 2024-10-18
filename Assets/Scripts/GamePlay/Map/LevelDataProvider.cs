using DefaultNamespace;
using GamePlay.TutorialStates;
using UnityEngine;

namespace GamePlay.Map
{
    public class LevelDataProvider : MonoBehaviour,ILevelDataProvider
    {
        [SerializeField] private LevelSO _levelData;
        [SerializeField] private TutorialSo _tutorialData;
        [SerializeField] private GameItemsSO _gameItems;
        public ILevelData GetLevel()
        {
            return _levelData.LevelData;
        }

        public IMapData GetMapData()
        {
            return _levelData.MapData;
        }

        public ITutorialData GetTutorialData()
        {
            return _tutorialData;
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