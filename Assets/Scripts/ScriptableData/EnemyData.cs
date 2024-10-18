using UnityEngine;

[System.Serializable]
public class EnemyData
{
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private string _poolKey;
    
    public float Health => _health;
    public float Speed => _speed;
    public string PoolKey => _poolKey;
}