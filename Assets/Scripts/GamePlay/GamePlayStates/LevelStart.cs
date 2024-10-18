using System;
using GamePlay.Spawner;
using General.StateMachines;
using UnityEngine;

namespace GamePlay.GamePlayStates
{
    public class LevelStart : GameState
    {
        [SerializeField] private EnemySpawner _spawner;
        public override void PlayState(Action<IState> onStateCompleted)
        {
            _spawner.StartRandomSpawning();
        }
    }
}