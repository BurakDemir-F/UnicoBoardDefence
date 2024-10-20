using System;
using GamePlay.Spawner;
using General.StateMachines;
using UnityEngine;

namespace GamePlay.GamePlayStates
{
    public class LevelStart : GameState
    {
        [SerializeField] private EnemyController _enemyController;
        public override void PlayState(Action<IState> onStateCompleted)
        {
            _enemyController.StartEnemySpawn();
            onStateCompleted?.Invoke(this);
        }
    }
}