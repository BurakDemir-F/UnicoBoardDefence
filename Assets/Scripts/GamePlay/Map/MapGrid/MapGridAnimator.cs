using System;
using System.Collections;
using System.Collections.Generic;
using General.GridSystem;
using UnityEngine;

namespace GamePlay.Map.MapGrid
{
    public class MapAnimator : MonoBehaviour , IMapAnimator
    {
        public void PlayPlacementAnimation(List<AreaBase> cells, GridPositionProvider positionProvider,float placementInterval, Action onPlacementEnd)
        {
            StartCoroutine(PlaceCor(cells, positionProvider, placementInterval, onPlacementEnd));
        }

        private IEnumerator PlaceCor(List<AreaBase> cells, GridPositionProvider positionProvider,float placementInterval, Action onPlacementEnd)
        {
            var wait = new WaitForSeconds(placementInterval);
            foreach (var gridCell in cells)
            {
                gridCell.Position = positionProvider.GetWorldPosition(gridCell.PositionOnGrid());
                yield return wait;
            }
            
            onPlacementEnd?.Invoke();
        }
    }

    public interface IMapAnimator
    {
        void PlayPlacementAnimation(List<AreaBase> cells, GridPositionProvider positionProvider,float placementInterval ,Action onPlacementEnd);
    }
}