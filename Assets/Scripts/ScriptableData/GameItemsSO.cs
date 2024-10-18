using System.Collections.Generic;
using Defenders;
using Enemies;
using General;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "ScriptableData/GameItems", fileName = "GameItems", order = 0)]
    public class GameItemsSO : ScriptableObject, IEnemyProperties,IDefenderProperties
    {
        [SerializeField] private EnemyTypeDataDict _enemyProperties;
        [SerializeField] private DefenderTypeDataDict _defenderProperties;
        
        public Dictionary<EnemyType, EnemyData> EnemyProperties => _enemyProperties;
        public Dictionary<DefenderType, DefenderData> DefenderProperties => _defenderProperties;
    }

    public interface IEnemyProperties
    {
        Dictionary<EnemyType, EnemyData> EnemyProperties { get; }
    }

    public interface IDefenderProperties
    {
        Dictionary<DefenderType, DefenderData> DefenderProperties { get; }
    }
    
    [System.Serializable]
    public class EnemyTypeDataDict : SerializableDictionary<EnemyType,EnemyData>{}
    
    [System.Serializable]
    public class DefenderTypeDataDict : SerializableDictionary<DefenderType,DefenderData>{}
}