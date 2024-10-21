using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay.Areas;
using GamePlay.Areas.Trigger;
using GamePlay.Map.MapGrid;
using General;
using General.GridSystem;
using UnityEngine;

namespace GamePlay.Map
{
    [CreateAssetMenu(menuName = "ScriptableData/Map", fileName = "Map", order = 0)]
    public class MapSO : ScriptableObject,IMap
    {
        public IGrid Grid { get; private set; }
        public List<SpawnArea> SpawnAreas { get; private set; }
        public List<NonDefenderArea> NonDefenderAreas { get; private set; }
        public List<DefenderArea> DefenderAreas { get; private set; }
        public List<PlayerLooseArea> PlayerLooseAreas { get; private set; }

        private HashSet<ITriggerBox> _triggerBoxes;
        public event Action<ITriggerInfo> AreaTriggerEntered;
        public event Action<ITriggerInfo> AreaTriggerExited;

        public event Action MapInitialized;

        public void InitializeMap(IGrid grid,
            List<SpawnArea> spawnAreas,
            List<NonDefenderArea> emptyAreas,
            List<DefenderArea> defenderAreas,
            List<PlayerLooseArea> playerLooseAreas)
        {
            Grid = grid;
            SpawnAreas = spawnAreas;
            NonDefenderAreas = emptyAreas;
            DefenderAreas = defenderAreas;
            PlayerLooseAreas = playerLooseAreas;
            _triggerBoxes = new HashSet<ITriggerBox>();
            MapInitialized?.Invoke();
        }

        public void AddTriggerBox(ITriggerBox triggerBox)
        {
            var isAdded = _triggerBoxes.Add(triggerBox);
            if (isAdded)
            {
                triggerBox.TriggerEnter += OnAreaTriggerEnter;
                triggerBox.TriggerExit += OnAreaTriggerExit;
            }
        }

        public void RemoveTriggerBox(ITriggerBox triggerBox)
        {
            var isRemoved = _triggerBoxes.Remove(triggerBox);
            if (isRemoved)
            {
                triggerBox.TriggerEnter -= OnAreaTriggerEnter;
                triggerBox.TriggerExit -= OnAreaTriggerExit;
            }
        }

        private void OnAreaTriggerEnter(ITriggerInfo info)
        {
            AreaTriggerEntered?.Invoke(info);
        }
        
        private void OnAreaTriggerExit(ITriggerInfo info)
        {
            AreaTriggerExited?.Invoke(info);
        }

        public IEnumerator<AreaBase> GetEnumerator()
        {
            foreach (var spawnArea in SpawnAreas)
                yield return spawnArea;

            foreach (var looseArea in PlayerLooseAreas)
                yield return looseArea;
            
            foreach (var defenderArea in DefenderAreas)
                yield return defenderArea;
            
            foreach (var nonDefenderArea in NonDefenderAreas)
                yield return nonDefenderArea;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public float GetOneAreaLength()
        {
            return Vector3.Magnitude(SpawnAreas[0].CenterPosition - SpawnAreas[1].CenterPosition);
        }
    }
}