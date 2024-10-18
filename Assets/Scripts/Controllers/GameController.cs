using GamePlay;
using GamePlay.Map;
using GamePlay.Map.MapGrid;
using GamePlay.Spawner;
using UnityEngine;
using Utilities;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _spawner;
        [SerializeField] private LevelDataProvider _dataProvider;
        [SerializeField] private MapBuilder _mapBuilder;
        [SerializeField] private GamePlayEventBus _eventBus;

        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            var mapData = _dataProvider.GetMapData();
            _mapBuilder.BuildMap(mapData, OnMapBuild);
        }

        private void OnMapBuild(IMap map)
        {
            var enemyData = _dataProvider.GetEnemyData();
            var levelData = _dataProvider.GetLevel();
            _spawner.Initialize(map,levelData,enemyData);
            _eventBus.Publish(GamePlayEvent.LevelSelected,new LevelSelectedEventInfo(0));
            _eventBus.Publish(GamePlayEvent.LevelStarted,null);
        }
    }
    
    public interface IUserDataController
    {
        int GetUserLevel();
        void SetUserLevel();
    }
}