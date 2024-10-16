using System;

namespace General.GridSystem
{
    public class GridCellCreateData
    {
        private Dimension _dimension;
        private Func<int, int, IGridCell> _createMethod;

        public Dimension Dimension
        {
            get => _dimension;
            set => _dimension = value;
        }
        
        public Func<int, int, IGridCell> InstantiateMethod
        {
            get => _createMethod;
            set => _createMethod = value;
        }

        public GridCellCreateData(Dimension dimension, Func<int, int, IGridCell> createMethod)
        {
            _dimension = dimension;
            _createMethod = createMethod;
        }
    }
}