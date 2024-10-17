using System.Collections.Generic;
using Defenders;
using Enemies;
using GamePlay.Map;
using General;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class LevelData : ILevelData,IMapData
{
    [SerializeField] private List<LevelQuantity> _levelQuantity;
    [SerializeField] private Dimension2D _defenderDimension;
    [SerializeField] private Dimension2D _emptyDimension;
    [SerializeField] private string _placeablePoolKey;
    [SerializeField] private string _nonPlaceablePoolKey;
    [SerializeField] private string _spawnAreaPoolKey;
    [SerializeField] private string _playerLooseAreaPoolKey;
    [SerializeField] private float _cellSize;
    [SerializeField] private float _padding;
    [SerializeField] private float _gridCreateInterval;
    
    public List<LevelQuantity> LevelQuantities => _levelQuantity;
    public Dimension2D DefenderDimension => _defenderDimension;
    public Dimension2D EmptyDimension => _emptyDimension;
    public string DefenderAreaKey => _placeablePoolKey;
    public string EmptyAreaKey => _nonPlaceablePoolKey;
    public float GridCreateInterval => _gridCreateInterval;

    public string SpawnAreaKey => _spawnAreaPoolKey;
    public string PlayerLooseAreaKey => _playerLooseAreaPoolKey;
    public float CellSize => _cellSize;
    public float Padding => _padding;
}

public interface ILevelData
{
    List<LevelQuantity> LevelQuantities { get; }
}

[System.Serializable]
public class LevelQuantity
{
    [SerializeField] private EnemyTypeCountDict _enemies;
    [SerializeField] private DefenderTypeCountDict _defenders;

    public EnemyTypeCountDict Enemies => _enemies;
    public DefenderTypeCountDict Defenders => _defenders;
}


[System.Serializable]
public class EnemyTypeCountDict : SerializableDictionary<EnemyType, int>
{
}

[System.Serializable]
public class DefenderTypeCountDict : SerializableDictionary<DefenderType, int>
{
}