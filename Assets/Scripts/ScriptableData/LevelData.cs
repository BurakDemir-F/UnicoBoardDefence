using System.Collections.Generic;
using Controllers;
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
    [SerializeField] private Vector2Int _placeableSize;
    [SerializeField] private Vector2Int _nonPlaceableSize;
    [SerializeField] private string _placeablePoolKey;
    [SerializeField] private string _nonPlaceablePoolKey;
    [SerializeField] private float _gridCreateInterval;
    
    public List<LevelQuantity> LevelQuantities => _levelQuantity;
    public Vector2Int PlaceableSize => _placeableSize;
    public Vector2Int NonPlaceableSize => _nonPlaceableSize;
    public string PlaceablePoolKey => _placeablePoolKey;
    public string NonPlaceablePoolKey => _nonPlaceablePoolKey;
    public float GridCreateInterval => _gridCreateInterval;
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