using System;
using System.Collections;
using DefaultNamespace;
using Enemies;
using GamePlay.Areas;
using GamePlay.Enemies;
using GamePlay.Map.MapGrid;
using General;
using General.Pool.System;
using UnityEngine;
using Utilities;

namespace GamePlay.Spawner
{
    public class EnemySpawner : MonoBehaviour,IEnemySpawner
    {
        [SerializeField] private GamePlayEventBus _eventBus;
        [SerializeField] MasterPool _poolCollection;
        [SerializeField] private Transform _heightTransform;
        private IMap _map;

        private ILevelData _levelData;
        private IEnemyProperties _enemyData;
        private LevelProperties _currentLevel;
        private IDestinationProvider _destinationProvider;

        private Coroutine _spawnCor;
        private YieldInstruction _spawnWait;
        
        public event Action<EnemyBase> EnemySpawned;

        private void Awake()
        {
            _eventBus.Subscribe(GamePlayEvent.LevelSelected,OnLevelSelected);
            _destinationProvider = GetComponent<IDestinationProvider>();
        }

        private void OnDestroy()
        {
            _eventBus.UnSubscribe(GamePlayEvent.LevelSelected,OnLevelSelected);    
        }

        public void Initialize(IMap map,ILevelData levelData, IEnemyProperties enemyData)
        {
            _map = map;
            _levelData = levelData;
            _enemyData = enemyData;
        }
        
        private void OnLevelSelected(IEventInfo eventInfo)
        {
            var info = (LevelSelectedEventInfo)eventInfo;
            _currentLevel = _levelData.DefenderEnemyCounts[info.SelectedLevel];
            _spawnWait = new WaitForSeconds(_currentLevel.EnemySpawnInterval);
        }

        public void StartRandomSpawning()
        {
            _spawnCor = StartCoroutine(SpawnCor());
        }

        public void StopSpawning()
        {
            if (_spawnCor == null)
            {
                StopCoroutine(_spawnCor);
            }
        }

        private IEnumerator SpawnCor()
        {
            foreach (var (enemyType, count) in _currentLevel.Enemies)
            {
                for (int i = 0; i < count; i++)
                {
                    var randomArea = _map.SpawnAreas.GetRandom();
                    SpawnAt(randomArea,enemyType);
                    yield return _spawnWait;
                }
            }
        }

        public void SpawnAt(AreaBase spawnArea, EnemyType enemyType)
        {
            var key = _enemyData.EnemyProperties[enemyType].PoolKey;
            var enemy = _poolCollection.Get<EnemyBase>(key);
            var startPosition = spawnArea.CenterPosition.SetY(_heightTransform.position.y); 
            enemy.Position = startPosition;
            var destination = _destinationProvider.GetDestination(spawnArea, _map);
            var enemyData = _enemyData.EnemyProperties[enemyType];
            enemy.ActivateEnemy(enemyData,destination);
            EnemySpawned?.Invoke(enemy);
        }

    }

    public interface IEnemySpawner
    {
        void StartRandomSpawning();
        void StopSpawning();
        void SpawnAt(AreaBase spawnArea,EnemyType enemyType);
        event Action<EnemyBase> EnemySpawned;
    }
}