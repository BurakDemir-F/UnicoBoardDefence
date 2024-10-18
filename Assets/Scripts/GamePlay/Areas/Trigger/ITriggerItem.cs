using UnityEngine;

namespace GamePlay.Areas
{
    public interface ITriggerItem
    {
        TriggerItemType TriggerItemType { get; }
        GameObject TriggerObject { get; }
    }
}