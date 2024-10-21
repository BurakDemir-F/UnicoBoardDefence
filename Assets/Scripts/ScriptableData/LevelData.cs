using System.Collections.Generic;
using GamePlay.Defenders;
using GamePlay.Enemies;
using General;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class LevelData : ILevelData
{
    [SerializeField] private List<LevelProperties> _levels;
    public List<LevelProperties> DefenderEnemyCounts => _levels;
}

public interface ILevelData
{
    List<LevelProperties> DefenderEnemyCounts { get; }
}

[System.Serializable]
public class LevelProperties
{
    [SerializeField] private EnemyTypeCountDict _enemies;
    [SerializeField] private DefenderTypeCountDict _defenders;
    [SerializeField] private float _enemySpawnInterval;

    public EnemyTypeCountDict Enemies => _enemies;
    public DefenderTypeCountDict Defenders => _defenders;
    public float EnemySpawnInterval => _enemySpawnInterval;
}

[System.Serializable]
public class EnemyTypeCountDict : SerializableDictionary<EnemyType, int>
{
}

[System.Serializable]
public class DefenderTypeCountDict : SerializableDictionary<DefenderType, int>
{
}
