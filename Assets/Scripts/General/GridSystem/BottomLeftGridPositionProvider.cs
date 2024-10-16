using UnityEngine;
using Utilities;

namespace General.GridSystem
{
    public class BottomLeftGridPositionProvider : GridPositionProvider
    {
        public BottomLeftGridPositionProvider( Transform originTransform, float cellSize, Transform root,
            float padding,Axis axis) : base(originTransform, cellSize, root, padding,axis)
        {
            
        }

        public override Vector3 GetWorldPosition(Vector2Int positionOnGrid)
        {
            var x = positionOnGrid.x;
            var y = positionOnGrid.y;
            var cellSizeOffset = GetCellSizeOffset();

            if (_axis == Axis.XY)
            {
                return new Vector3(_originPosition.x + (x * _cellSize) + cellSizeOffset,
                    _originPosition.y + ( y * _cellSize) + cellSizeOffset, 0f);
            } 
            
            return new Vector3(_originPosition.x + (x * _cellSize) + cellSizeOffset,
                _originPosition.y , _originPosition.z + (y * _cellSize) + cellSizeOffset);
        }
    }
}