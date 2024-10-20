using GamePlay.Areas;
using GamePlay.Map.MapGrid;
using UnityEngine;
using Utilities;

namespace GamePlay.Enemies
{
    public class EnemyDestinationProvider : MonoBehaviour,IDestinationProvider
    {
        public Vector3 GetDestination(AreaBase currentArea, IMap map)
        {
            foreach (var looseArea in map.PlayerLooseAreas)
            {
                if (looseArea.XPos == currentArea.XPos)
                    return looseArea.CenterPosition;
            }

            return Vector3.one * -1f;
        }
    }

    public interface IDestinationProvider
    {
        Vector3 GetDestination(AreaBase currentArea,IMap map);
    }
}