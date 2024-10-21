using UnityEngine;

namespace GamePlay.Areas.Trigger
{
    public interface ITriggerItem
    {
        TriggerItemType TriggerItemType { get; }
        GameObject TriggerObject { get; }
    }
}