using GamePlay.Map;
using General;
using UnityEngine;

[System.Serializable]
public class MapData : IMapData
{
    [SerializeField] private Dimension2D _defenderDimension;
    [SerializeField] private Dimension2D _emptyDimension;
    [SerializeField] private string _defenderAreaPoolKey;
    [SerializeField] private string _emptyAreaPoolKey;
    [SerializeField] private string _spawnAreaPoolKey;
    [SerializeField] private string _playerLooseAreaPoolKey;
    [SerializeField] private float _cellSize;
    [SerializeField] private float _padding;
    [SerializeField] private float _gridCreateInterval;
    
    public Dimension2D DefenderDimension => _defenderDimension;
    public Dimension2D EmptyDimension => _emptyDimension;
    public string DefenderAreaKey => _defenderAreaPoolKey;
    public string EmptyAreaKey => _emptyAreaPoolKey;
    public float GridCreateInterval => _gridCreateInterval;

    public string SpawnAreaKey => _spawnAreaPoolKey;
    public string PlayerLooseAreaKey => _playerLooseAreaPoolKey;
    public float CellSize => _cellSize;
    public float Padding => _padding;
}