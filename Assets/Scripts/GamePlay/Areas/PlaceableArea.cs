using Defenders;

namespace GamePlay.Areas
{
    public class PlaceableArea : GameArea
    {
        public IAreaPlaceable PlacedItem { get; private set; }
        public bool IsPlaced { get; private set; }
        public void PlaceItem(IAreaPlaceable item)
        {
            item.Place(CenterPosition);
            PlacedItem = item;
            IsPlaced = true;
        }
    }
}