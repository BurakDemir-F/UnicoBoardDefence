using System;

namespace General.GridSystem
{
    public class GridCellCreateData
    {
        private Dimension2D _dimension2D;
        private Func<int, int, IGridCell> _createMethod;

        public Dimension2D Dimension2D
        {
            get => _dimension2D;
            set => _dimension2D = value;
        }
        
        public Func<int, int, IGridCell> InstantiateMethod
        {
            get => _createMethod;
            set => _createMethod = value;
        }

        public GridCellCreateData(Dimension2D dimension2D, Func<int, int, IGridCell> createMethod)
        {
            _dimension2D = dimension2D;
            _createMethod = createMethod;
        }
    }
}