using UnityEngine;

namespace General
{
    [System.Serializable]
    public struct Dimension2D
    {
        [SerializeField] private int _xLength;
        [SerializeField] private int _yLength;

        public int XLength => _xLength;
        public int YLength => _yLength;

        public int Size => _xLength * _yLength;
        
        public Dimension2D(int xLength, int yLength)
        {
            _xLength = xLength;
            _yLength = yLength;
        }
    }
}