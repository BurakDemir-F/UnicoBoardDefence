using System;
using System.Collections.Generic;
using GamePlay.Areas;
using GamePlay.Enemies;
using GamePlay.Map;
using General;
using UnityEngine;

namespace GamePlay.Spawner
{
    public class EnemyController : MonoBehaviour,IEnemyController
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private GamePlayEventBus _eventBus;
        [SerializeField] private MapSO _map;
        
        private HashSet<EnemyBase> _spawnedEnemies = new();
        private Dictionary<EnemyBase, AreaBase> _enemyAreaDict = new();
        public Dictionary<EnemyBase, AreaBase> EnemyAreaDictionary => _enemyAreaDict;

        public event Action<EnemyBase> EnemyDeath; 
        private void Awake()
        {
            _map.AreaTriggerEntered += OnAreaEnter;
            _map.AreaTriggerExited += OnAreaExit;
            _enemySpawner.EnemySpawned += OnEnemySpawned;
            _eventBus.Subscribe(GamePlayEvent.LevelWin,OnLevelEnd);
            _eventBus.Subscribe(GamePlayEvent.LevelFail,OnLevelEnd);
        }

        private void OnDestroy()
        {
            _enemySpawner.EnemySpawned -= OnEnemySpawned;
            _map.AreaTriggerEntered -= OnAreaEnter;
            _map.AreaTriggerExited -= OnAreaExit;
            _eventBus.UnSubscribe(GamePlayEvent.LevelWin,OnLevelEnd);
            _eventBus.UnSubscribe(GamePlayEvent.LevelFail,OnLevelEnd);
        }

        public void StartEnemySpawn()
        {
            _enemySpawner.StartRandomSpawning();
        }

        private void OnAreaEnter(ITriggerInfo info)
        {
            if(info.TriggerItem.TriggerItemType != TriggerItemType.Enemy)
                return;
            var enemy = info.TriggerItem.TriggerObject.GetComponent<EnemyBase>();
            var area = info.TriggeredArea;
            _enemyAreaDict.TryAdd(enemy, area);
        }

        private void OnEnemySpawned(EnemyBase enemyBase)
        {
            enemyBase.EnemyDeath += OnEnemyDeath;
            _spawnedEnemies.Add(enemyBase);
        }
        
        private void OnEnemyDeath(EnemyBase enemyBase)
        {
            _enemyAreaDict.Remove(enemyBase);
            _spawnedEnemies.Remove(enemyBase);
            enemyBase.ReturnToPool();
            EnemyDeath?.Invoke(enemyBase);
        }
        
        private void OnAreaExit(ITriggerInfo info)
        {
        }

        private void OnLevelEnd(IEventInfo eventInfo)
        {
            foreach (var spawnedEnemy in _spawnedEnemies)
            {
                spawnedEnemy.ReturnToPool();
            }
            
            _spawnedEnemies.Clear();
        }
    }

    public interface IEnemyController
    {
        Dictionary<EnemyBase, AreaBase> EnemyAreaDictionary { get; }
        void StartEnemySpawn();
    }
}