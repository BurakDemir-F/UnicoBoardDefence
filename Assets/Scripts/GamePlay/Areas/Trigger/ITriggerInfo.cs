namespace GamePlay.Areas
{
    public interface ITriggerInfo
    {
        AreaBase TriggeredArea { get; }
        ITriggerItem TriggerItem { get; }
    }
}