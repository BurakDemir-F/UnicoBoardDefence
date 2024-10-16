namespace General.GridSystem
{
    public struct Dimension
    {
        private int _xLength;
        private int _yLength;

        public int XLength => _xLength;
        public int YLength => _yLength;
        
        public Dimension(int xLength, int yLength)
        {
            _xLength = xLength;
            _yLength = yLength;
        }
    }
}