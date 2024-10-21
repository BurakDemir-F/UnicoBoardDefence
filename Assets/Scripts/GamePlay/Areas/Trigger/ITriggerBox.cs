using System;

namespace GamePlay.Areas.Trigger
{
    public interface ITriggerBox
    {
        event Action<ITriggerInfo> TriggerEnter;
        event Action<ITriggerInfo> TriggerExit;
    }
}