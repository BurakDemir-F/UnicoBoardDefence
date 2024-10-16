using System.Collections.Generic;
using Defenders;
using Enemies;
using General;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableData/Level", fileName = "Level", order = 0)]
public class LevelSO : ScriptableObject
{
    private LevelData _levels;
}