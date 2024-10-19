﻿using GamePlay.Areas;
using GamePlay.Enemies;
using UnityEngine;

namespace Enemies
{
    public class EnemyTriggerItem : MonoBehaviour, ITriggerItem
    {
        [SerializeField] private EnemyBase _enemy;
        public TriggerItemType TriggerItemType => TriggerItemType.Enemy;
        public GameObject TriggerObject => _enemy.gameObject;
    }
}