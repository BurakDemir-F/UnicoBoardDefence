using General;
using UnityEngine;

[System.Serializable]
public class DefenderData
{
    [SerializeField] private int _range;
    [SerializeField] private Direction _direction;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackInterval;
    [SerializeField] private float _buildTime;
    [SerializeField] private string _poolKey;
    public int Range => _range;
    public Direction Direction => _direction;
    public float Damage => _damage;
    public float AttackInterval => _attackInterval;
    public float BuildTime => _buildTime;
    public string PoolKey => _poolKey;
}