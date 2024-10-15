using System.Collections.Generic;
using Defenders;
using Enemies;
using General;
using UnityEngine;

[System.Serializable]
public class LevelData: ILevelData
{
    [SerializeField] private EnemyTypeCountDict _enemies;
    [SerializeField] private DefenderTypeCountDict _defenders;
    
    public Dictionary<DefenderType, int> DefendersDict => _defenders;
    public Dictionary<EnemyType, int> EnemiesDict => _enemies;
}

public interface ILevelData
{
    Dictionary<DefenderType, int> DefendersDict { get; }
    Dictionary<EnemyType, int> EnemiesDict { get; }
}


[System.Serializable]
public class EnemyTypeCountDict : SerializableDictionary<EnemyType, int>
{
}

[System.Serializable]
public class DefenderTypeCountDict : SerializableDictionary<DefenderType, int>
{
}