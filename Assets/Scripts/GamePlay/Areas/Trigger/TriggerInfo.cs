namespace GamePlay.Areas.Trigger
{
    public struct TriggerInfo : ITriggerInfo
    {
        private AreaBase _triggeredArea;
        private ITriggerItem _triggerItem;
        
        public AreaBase TriggeredArea => _triggeredArea;
        public ITriggerItem TriggerItem => _triggerItem;

        public TriggerInfo( AreaBase triggeredArea, ITriggerItem triggerItem)
        {
            _triggeredArea = triggeredArea;
            _triggerItem = triggerItem;
        }
    }
}