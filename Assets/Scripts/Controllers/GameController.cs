using System;
using GamePlay;
using GamePlay.GamePlayStates;
using GamePlay.Map;
using General;
using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private LevelController _levelController;
        [SerializeField] private PlayUI _playUI;
        [SerializeField] private GamePlayEventBus _eventBus;
        [SerializeField] private LevelDataProvider _dataProvider;

        private void Start()
        {
            _playUI.PlayButtonClicked += StartLevel;
            _eventBus.Subscribe(GamePlayEvent.LevelEnd,OnLevelEnd);
            ShowPlayGame();
        }

        private void OnDestroy()
        {
            _playUI.PlayButtonClicked -= StartLevel;
            _eventBus.UnSubscribe(GamePlayEvent.LevelEnd,OnLevelEnd);
        }

        private void StartLevel()
        {
            _playUI.Deactivate();
            var levelData = _dataProvider.GetLevel();
            var currentLevel = _levelController.GetLevel();
            _eventBus.Publish(GamePlayEvent.LevelSelected,
                new LevelSelectedEventInfo(currentLevel,
                    levelData.DefenderEnemyCounts[currentLevel]));
            _eventBus.Publish(GamePlayEvent.LevelStarted,null);
        }

        private void ShowPlayGame()
        {
            _playUI.Activate();
            _playUI.ShowLevel(_levelController.GetLevelForDisplay());
        }

        private void OnLevelEnd(IEventInfo eventInfo)
        {
            ShowPlayGame();
        }

    }
}