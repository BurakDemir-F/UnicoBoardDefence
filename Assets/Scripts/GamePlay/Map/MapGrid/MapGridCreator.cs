using System;
using System.Collections;
using System.Collections.Generic;
using General;
using General.GridSystem;
using General.Pool.System;
using UnityEngine;

namespace GamePlay.Map.MapGrid
{
    public class MapGridCreator : MonoBehaviour,IConstructionProvider
    {
        [SerializeField] private Transform _mapRoot;

        private List<IGridCell> _cells;
        private IPoolCollection _poolCollection;
        private YieldInstruction _intervalWait;
        private Action<List<IGridCell>> _onGridCreated;
        public void Construct()
        {
            _cells = new List<IGridCell>();
            _poolCollection = GetComponent<IPoolCollection>();
        }

        public void Destruct()
        {
            
        }

        public void CreateMapGrid(IMapData mapData,Action<List<IGridCell>> onGridCreated)
        {
            _onGridCreated = onGridCreated;
            _intervalWait = new WaitForSeconds(mapData.GridCreateInterval);
            StartCoroutine(CreateMapGridCor(mapData));
        }

        private IEnumerator CreateMapGridCor(IMapData mapData)
        {
            var nonPlaceableSize = mapData.NonPlaceableSize;
            var nonPlaceableKey = mapData.NonPlaceablePoolKey;
            var placeableKey = mapData.PlaceablePoolKey;
            var count = nonPlaceableSize.x * nonPlaceableSize.y;

            for (var i = 0; i < count; i++)
            {
                var cell = _poolCollection.Get<EmptyArea>(nonPlaceableKey);
                _cells.Add(cell);
                yield return _intervalWait;
            }

            var placeableSize = mapData.PlaceableSize;
            count = placeableSize.x * placeableSize.y;
            
            for (var i = 0; i < count; i++)
            {
                var cell = _poolCollection.Get<DefenderBaseArea>(placeableKey);
                _cells.Add(cell);
                yield return _intervalWait;
            }
            
            _onGridCreated?.Invoke(_cells);
        }
        
    }
}