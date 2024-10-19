namespace GamePlay.Areas
{
    public class GameArea : AreaBase
    {
        private IAreaIndicator _areaIndicator;
        public IAreaIndicator AreaIndicator => _areaIndicator;
        
        public override void OnGetFromPool()
        {
            base.OnGetFromPool();
            _areaIndicator = GetComponent<IAreaIndicator>();
        }
    }
}