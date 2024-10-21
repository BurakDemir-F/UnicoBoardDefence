using System;
using UnityEngine;

namespace GamePlay.Enemies
{
    public interface IEnemy
    {
        event Action<IEnemy> EnemyDeath; 
        Transform Transform { get; }
    }
}