using GamePlay.TutorialStates;
using UnityEngine;

namespace GamePlay.Map
{
    public class LevelDataProvider : MonoBehaviour,ILevelDataProvider
    {
        [SerializeField] private LevelSO _levelData;
        [SerializeField] private TutorialSo _tutorialData;
        public ILevelData GetLevel()
        {
            return _levelData.LevelData;
        }

        public IMapData GetMapData()
        {
            return _levelData.LevelData;
        }

        public ITutorialData GetTutorialData()
        {
            return _tutorialData;
        }
    }
}