namespace GamePlay.Areas.Trigger
{
    public interface ITriggerInfo
    {
        AreaBase TriggeredArea { get; }
        ITriggerItem TriggerItem { get; }
    }
}