using System;

namespace GamePlay.Areas
{
    public interface ITriggerBox
    {
        event Action<ITriggerInfo> TriggerEnter;
        event Action<ITriggerInfo> TriggerExit;
    }
}