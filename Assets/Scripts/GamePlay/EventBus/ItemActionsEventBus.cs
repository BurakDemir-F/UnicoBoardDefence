using General;
using UnityEngine;

namespace GamePlay.EventBus
{
    [CreateAssetMenu(menuName = "ScriptableData/ItemActions", fileName = "ItemActions", order = 0)]
    public class ItemActionsEventBus : EventBus<ItemActions>
    {
        
    }

    public enum ItemActions
    {
        None,
        RemainingDefenceItemsChanged,
        DefenceItemSelected,
        AllEnemiesDead,
    }
}