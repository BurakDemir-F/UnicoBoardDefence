﻿using System.Collections.Generic;
using Defenders;
using GamePlay.Areas;
using General;
using UnityEngine;

namespace GamePlay.Map.MapGrid
{
    public class AreaController : MonoBehaviour,IAreaController
    {
        private IMap _map;
        private HashSet<DefenderArea> _placedAreas = new();
        
        public void Initialize(IMap map)
        {
            _map = map;
        }
        
        public bool IsPlaceable(DefenderArea area)
        {
            return !_placedAreas.Contains(area);
        }

        public void Place(DefenderArea area, DefenceItemBase defenceItem)
        {
            area.PlaceItem(defenceItem);
            _placedAreas.Add(area);
        }

        public HashSet<GameArea> GetInRangeAreas(DefenderArea placedArea, DefenderData data)
        {
            var range = data.Range;
            var direction = data.Direction;
            
            return direction == Direction.All
                ? GetFromAllDirections(range, placedArea)
                : GetFromDirection(range, direction, placedArea);
        }

        public void IndicateInRangeAreas(HashSet<GameArea> inRangeAreas)
        {
            foreach (var inRangeArea in inRangeAreas)
            {
                inRangeArea.AreaIndicator.ActivateRangeIndicator();
            }
        }

        private HashSet<GameArea> GetFromAllDirections(int range, DefenderArea placedArea)
        {
            var inRangeAreas = new HashSet<GameArea>();
            var grid = _map.Grid;

            for (int i = 0; i < range; i++)
            {
                var neighbors = grid.GetNeighbors(placedArea);
                foreach (var neighbor in neighbors)
                {
                    var cell = neighbor.Cell;
                    if(cell is GameArea area)
                        inRangeAreas.Add(area);
                }
            }

            return inRangeAreas;
        }

        private HashSet<GameArea> GetFromDirection(int range,Direction direction, DefenderArea placedArea)
        {
            var inRangeAreas = new HashSet<GameArea>();
            var grid = _map.Grid;
            for (int i = 0; i < range; i++)
            {
                var hasNextArea = grid.TryGetNextCell(placedArea, direction, out var nextCell);
                if (hasNextArea)
                {
                    if(nextCell is GameArea area)
                        inRangeAreas.Add(area);
                }
            }
            return inRangeAreas;
        }
    }
}