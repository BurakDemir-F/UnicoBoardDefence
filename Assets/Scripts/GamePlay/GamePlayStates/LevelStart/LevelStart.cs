using System;
using Controllers;
using GamePlay.EventBus;
using GamePlay.EventBus.Info;
using GamePlay.Map;
using GamePlay.Map.MapGrid;
using General.Pool.System;
using General.StateMachines;
using UnityEngine;

namespace GamePlay.GamePlayStates.LevelStart
{
    public class LevelStart : GameState
    {
        [SerializeField] private LevelDataProvider _dataProvider;
        [SerializeField] private LevelController _levelController;
        [SerializeField] private MapBuilder _mapBuilder;
        [SerializeField] private MapController _mapController;
        [SerializeField] private MasterPool _poolCollection;
        [SerializeField] private EnemyController _enemyController;
        
        private Action<IState> _onStateCompleted;
        
        public override void PlayState(Action<IState> onStateCompleted)
        {
            var mapData = _dataProvider.GetMapData();
            _mapBuilder.BuildMap(mapData, OnMapBuild);
            _onStateCompleted = onStateCompleted;
        }
        
        private void OnMapBuild(IMap map)
        {
            var enemyData = _dataProvider.GetEnemyData();
            _enemyController.Initialize(map,enemyData);
            _mapController.Initialize(map,_poolCollection);
            _enemyController.StartEnemySpawn();
            _eventBus.Publish(GamePlayEvent.MapCreated,new MapCreatedEventInfo(map));
            _onStateCompleted?.Invoke(this);
        }
    }
}