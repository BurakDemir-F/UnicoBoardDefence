using GamePlay.Map;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableData/Level", fileName = "Level", order = 0)]
public class LevelSO : ScriptableObject
{
    [SerializeField] private LevelData _levelData;
    [SerializeField] private MapData _mapData;
    public ILevelData LevelData => _levelData;
    public IMapData MapData => _mapData;
}