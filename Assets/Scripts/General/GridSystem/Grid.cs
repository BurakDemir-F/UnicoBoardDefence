using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace General.GridSystem
{
    public class Grid : IEnumerable<IGridCell>
    {
        protected IGridCell[,] _items;
        protected int _xDimension;
        protected int _yDimension;
        
        public Grid(List<IGridCell> cells, int xDimension,int yDimension)
        {
            _xDimension = xDimension;
            _yDimension = yDimension;

            _items = new IGridCell[_xDimension, _yDimension];

            for (int i = 0; i < xDimension; i++)
            {
                for (int j = 0; j < yDimension; j++)
                {
                    _items[i, j] = cells[GetIndex(_xDimension, i, j)];
                }
            }
        }

        private int GetIndex(int xDimension, int xPos, int yPos)
        {
            return xDimension * yPos + xPos;
        }
        
        public void SetCell(IGridCell cell)
        {
            _items[cell.XPos, cell.YPos] = cell;
        }

        public virtual int GetSize()
        {
            return _xDimension * _yDimension;
        }

        public virtual Vector2Int GetDimensions()
        {
            return new Vector2Int(_xDimension, _yDimension);
        }

        public virtual IGridCell GetLast()
        {
            return this[_xDimension - 1, _yDimension - 1];
        }

        public virtual IGridCell GetFirst()
        {
            return this[0, 0];
        }

        protected virtual Vector2Int GetNextPosition(IGridCell currentCell)
        {
            var x = currentCell.XPos;
            var y = currentCell.YPos;

            var lastX = _xDimension - 1;
            var lastY = _yDimension - 1;
            if (x > lastX && y > lastY || x <= lastX && y > lastY)
                return Vector2Int.one * -1;

            if (x < _xDimension - 1)
                return new Vector2Int(x + 1, y);

            return new Vector2Int(0, y + 1);
        }

        public virtual bool TryGetPreviousCell(IGridCell currentCell, out IGridCell previousCell)
        {
            var x = currentCell.XPos;
            var y = currentCell.YPos;

            var lastX = _xDimension - 1;
            var lastY = _yDimension - 1;

            var isFirst = y == 0 && x == 0;
            var isInBoundary = x > lastX && y > lastY || x <= lastX && y > lastY;
            if (isFirst || !isInBoundary)
            {
                previousCell = default;
                return false;
            }

            if (y != 0)
            {
                previousCell = this[x-1, y];
                return true;
            }

            previousCell = this[y - 1, lastX];
            return true;
        }

        public virtual bool TryGetNextCell(IGridCell currentCell, out IGridCell nextCell)
        {
            var nextPos = GetNextPosition(currentCell);

            if (nextPos == Vector2Int.one * -1)
            {
                nextCell = null;
                return false;
            }
            
            nextCell = this[nextPos.x, nextPos.y];
            return true;
        }

        public virtual IGridCell this[int x, int y]
        {
            get
            {
                if (x >= _xDimension || y >= _yDimension)
                    throw new ArgumentOutOfRangeException($"x value: {x}, y value: {y}");

                return _items[x, y];
            }
            set
            {
                if (x >= _xDimension || y >= _yDimension)
                    throw new ArgumentOutOfRangeException($"x value: {x}, y value: {y}");
                _items[x, y] = value;
            }
        }

        public List<IGridCell> GetNeighbors(IGridCell cell)
        {
            var neighbors = new List<IGridCell>();

            var x = cell.XPos;
            var y = cell.YPos;

            if (IsSafe(x + 1, y)) neighbors.Add(this[x + 1, y]);
            if (IsSafe(x - 1, y)) neighbors.Add(this[x - 1, y]);
            if (IsSafe(x, y + 1)) neighbors.Add(this[x, y + 1]);
            if (IsSafe(x, y - 1)) neighbors.Add(this[x, y - 1]);

            return neighbors;

            bool IsSafe(int xPos, int yPos)
            {
                return (xPos >= 0 && xPos < _xDimension && yPos >= 0 && yPos < _yDimension);
            }
        }

        public IEnumerator<IGridCell> GetEnumerator()
        {
            for (var i = 0; i < _xDimension; i++)
            {
                for (var j = 0; j < _yDimension; j++)
                {
                    yield return _items[i, j];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}