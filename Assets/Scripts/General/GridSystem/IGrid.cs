using System.Collections.Generic;
using UnityEngine;

namespace General.GridSystem
{
    public interface IGrid : IEnumerable<IGridCell>
    {
        int GetSize();
        Vector2Int GetDimensions();
        IGridCell GetLast();
        IGridCell GetFirst();
        IGridCell this[int x, int y] { get; set; }
    }
}