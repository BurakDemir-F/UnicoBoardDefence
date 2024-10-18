using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay.Areas;
using General.GridSystem;
using UnityEngine;

namespace GamePlay.Map.MapGrid
{
    public class MapAnimator : MonoBehaviour , IMapAnimator
    {
        [SerializeField] private Transform _tempTransform;
        private Action _onPlacementEnd;
        public void PlayPlacementAnimation(List<AreaBase> cells, GridPositionProvider positionProvider,float placementInterval, Action onPlacementEnd)
        {
            _onPlacementEnd = onPlacementEnd;
            StartCoroutine(PlaceCor(cells, positionProvider, placementInterval));
        }

        private IEnumerator PlaceCor(List<AreaBase> areas, GridPositionProvider positionProvider,float placementInterval)
        {
            foreach (var area in areas)
            {
                area.Position = _tempTransform.position;
            }
            
            var wait = new WaitForSeconds(placementInterval);
            for (var i = 0; i < areas.Count - 1; i++)
            {
                var area = areas[i];
                area.Position = positionProvider.GetWorldPosition(area.PositionOnGrid());
                area.AnimatePlacing(null);
                yield return wait;
            }

            //just waiting for the finish of last animation
            var latestArea = areas[^1];
            latestArea.Position = positionProvider.GetWorldPosition(latestArea.PositionOnGrid());
            latestArea.AnimatePlacing(OnLatestAnimFinished);
        }

        private void OnLatestAnimFinished()
        {
            _onPlacementEnd?.Invoke();
        }
    }

    public interface IMapAnimator
    {
        void PlayPlacementAnimation(List<AreaBase> cells, GridPositionProvider positionProvider,float placementInterval ,Action onPlacementEnd);
    }
}