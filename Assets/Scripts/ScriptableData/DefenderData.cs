using Defenders;
using General;
using UnityEngine;

[System.Serializable]
public class DefenderData
{
    [SerializeField] private DefenderType _defenderType;
    [SerializeField] private int _range;
    [SerializeField] private Direction _direction;
    [SerializeField] private float _buildTime;
    [SerializeField] private string _poolKey;
    [SerializeField] private WeaponData _weaponData;
    public DefenderType DefenderType => _defenderType;
    public int Range => _range;
    public Direction Direction => _direction;
    public float BuildTime => _buildTime;
    public string PoolKey => _poolKey;
    public WeaponData WeaponData => _weaponData;
}

[System.Serializable]
public class WeaponData 
{
    [SerializeField] private float _damage;
    [SerializeField] private float _attackInterval;
    [SerializeField] private string _bulletKey;
    [SerializeField] private float _bulletSpeed;
    public float Damage => _damage;
    public float AttackInterval => _attackInterval;
    public string BulletKey => _bulletKey;
    public float BulletSpeed => _bulletSpeed;
}