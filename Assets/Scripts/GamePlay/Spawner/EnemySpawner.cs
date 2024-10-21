using System;
using System.Collections;
using System.Linq;
using GamePlay.Areas;
using GamePlay.Enemies;
using GamePlay.EventBus;
using GamePlay.EventBus.Info;
using GamePlay.Map.MapGrid;
using General;
using General.Pool.System;
using UnityEngine;
using Utilities;

namespace GamePlay.Spawner
{
    public class EnemySpawner : MonoBehaviour, IEnemySpawner
    {
        [SerializeField] private GamePlayEventBus _eventBus;
        [SerializeField] MasterPool _poolCollection;
        [SerializeField] private Transform _heightTransform;
        private IMap _map;

        private IEnemyProperties _enemyData;
        private LevelProperties _currentLevel;
        private IDestinationProvider _destinationProvider;

        private Coroutine _spawnCor;
        private YieldInstruction _spawnWait;
        private SpawnInfo _spawnInfo;
        public event Action<EnemyBase> EnemySpawned;

        private void Awake()
        {
            _eventBus.Subscribe(GamePlayEvent.LevelSelected, OnLevelSelected);
            _destinationProvider = GetComponent<IDestinationProvider>();
        }

        private void OnDestroy()
        {
            _eventBus.UnSubscribe(GamePlayEvent.LevelSelected, OnLevelSelected);
        }

        public void Initialize(IMap map, IEnemyProperties enemyData)
        {
            _map = map;
            _enemyData = enemyData;
        }

        private void OnLevelSelected(IEventInfo eventInfo)
        {
            var info = (LevelSelectedEventInfo)eventInfo;
            _currentLevel = info.LevelProperties;
            _spawnWait = new WaitForSeconds(_currentLevel.EnemySpawnInterval);
        }

        public void StartRandomSpawning()
        {
            _spawnInfo.IsSpawning = true;
            _spawnCor = StartCoroutine(SpawnCor());
        }

        public int GetEnemyCount()
        {
            var count = 0;
            foreach (var (enemyType, enemyCount) in _currentLevel.Enemies)
                count += enemyCount;
            return count;
        }

        public void StopSpawning()
        {
            _spawnInfo.Reset();
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
                    if (!_spawnInfo.IsSpawning)
                        yield break;
                    var randomArea = _map.SpawnAreas.GetRandom();
                    SpawnAt(randomArea, enemyType);
                    yield return _spawnWait;
                }
            }
        }

        public void SpawnAt(AreaBase spawnArea, EnemyType enemyType)
        {
            var key = _enemyData.EnemyProperties[enemyType].PoolKey;
            var enemy = _poolCollection.Get<EnemyBase>(key);
            var positionY = _heightTransform.position.y;
            var startPosition = spawnArea.CenterPosition.SetY(positionY);
            enemy.Position = startPosition;
            enemy.transform.SetParent(transform);
            var destination = _destinationProvider.GetDestination(spawnArea, _map);
            destination = destination.SetY(positionY);
            var enemyData = _enemyData.EnemyProperties[enemyType];
            enemy.ActivateEnemy(enemyData, startPosition, destination,_map.GetOneAreaLength());
            EnemySpawned?.Invoke(enemy);
        }

        private struct SpawnInfo
        {
            public bool IsSpawning;
            public float Counter;
            public float Duration;

            public void Reset()
            {
                IsSpawning = false;
                Counter = 0f;
                Duration = 0f;
            }
        }
    }

    public interface IEnemySpawner
    {
        void StartRandomSpawning();
        void StopSpawning();
        void SpawnAt(AreaBase spawnArea, EnemyType enemyType);
        event Action<EnemyBase> EnemySpawned;
    }
}