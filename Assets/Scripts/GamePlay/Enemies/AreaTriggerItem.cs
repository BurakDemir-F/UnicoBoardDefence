using GamePlay.Areas.Trigger;
using UnityEngine;

namespace GamePlay.Enemies
{
    public class AreaTriggerItem : MonoBehaviour, ITriggerItem
    {
        [SerializeField] private EnemyBase _enemy;
        public TriggerItemType TriggerItemType => TriggerItemType.Enemy;
        public GameObject TriggerObject => _enemy.gameObject;
    }
}