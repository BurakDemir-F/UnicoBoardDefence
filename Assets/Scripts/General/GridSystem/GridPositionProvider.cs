using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace General.GridSystem
{
    public abstract class GridPositionProvider
    {
        protected Transform _originTransform;
        protected Vector3 _originPosition;
        protected float _cellSize;
        protected float _padding;
        protected Axis _axis;

        protected GridPositionProvider(Transform originTransform, float cellSize, Transform root,
            float padding,Axis axis)
        {
            _originTransform = originTransform;
            _originPosition = _originTransform.position;
            _cellSize = cellSize;
            _padding = padding;
            _axis = axis;
            
            if (_axis == Axis.None)
                "Axis can not be None!".PrintColored(Color.red);
        }

        public abstract Vector3 GetWorldPosition(Vector2Int positionOnGrid);
        
        protected float GetCellSizeOffset()
        {
            return _cellSize * .5f;
        }
    }
}