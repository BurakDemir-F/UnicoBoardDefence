using GamePlay;
using General;
using UnityEngine;

namespace Controllers
{
    public class LevelController : MonoBehaviour, ILevelController
    {
        [SerializeField] private GamePlayEventBus _eventBus;
        [SerializeField] private string _levelPrefKey = "level";
        [SerializeField] private LevelSO _levels;

        private void Awake()
        {
            _eventBus.Subscribe(GamePlayEvent.LevelWin, OnLevelWin);
            _eventBus.Subscribe(GamePlayEvent.LevelFail, OnLevelFail);
        }

        private void OnDestroy()
        {
            _eventBus.UnSubscribe(GamePlayEvent.LevelWin, OnLevelWin);
            _eventBus.UnSubscribe(GamePlayEvent.LevelFail, OnLevelFail);
        }

        private void OnLevelWin(IEventInfo eventInfo)
        {
            var level = GetLevel();
            SetLevel(level+1);
        }

        private void OnLevelFail(IEventInfo eventInfo)
        {
            
        }
        
        public int GetLevel()
        {
            return PlayerPrefs.GetInt(_levelPrefKey);
        }

        public int GetLevelForDisplay()
        {
            return GetLevel() + 1;
        }

        public void SetLevel(int level)
        {
            var totalLevelCount = _levels.LevelData.DefenderEnemyCounts.Count;
            if (level > totalLevelCount - 1)
            {
                level = totalLevelCount - 1;
            }
            PlayerPrefs.SetInt(_levelPrefKey,level);
        }
    }

    public interface ILevelController
    {
        int GetLevel();
        void SetLevel(int level);
    }
}