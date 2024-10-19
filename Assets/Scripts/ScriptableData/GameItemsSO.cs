using System.Collections.Generic;
using Defenders;
using Enemies;
using General;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "ScriptableData/GameItems", fileName = "GameItems", order = 0)]
    public class GameItemsSO : ScriptableObject, IEnemyProperties,IDefenderProperties
    {
        [SerializeField] private EnemyTypeDataDict _enemyProperties;
        [SerializeField] private DefenderTypeDataDict _defenderItemProperties;
        
        public Dictionary<EnemyType, EnemyData> EnemyProperties => _enemyProperties;
        public Dictionary<DefenderType, DefenderData> DefenderItemProperties => _defenderItemProperties;
    }

    public interface IEnemyProperties
    {
        Dictionary<EnemyType, EnemyData> EnemyProperties { get; }
    }

    public interface IDefenderProperties
    {
        Dictionary<DefenderType, DefenderData> DefenderItemProperties { get; }
    }
    
    [System.Serializable]
    public class EnemyTypeDataDict : SerializableDictionary<EnemyType,EnemyData>{}
    
    [System.Serializable]
    public class DefenderTypeDataDict : SerializableDictionary<DefenderType,DefenderData>{}
}