﻿using System;
using GamePlay.Map;
using UnityEngine;
using Utilities;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        private ILevelDataProvider _dataProvider;
        private IMapBuilder _mapBuilder;

        private void Awake()
        {
            _dataProvider = GetComponent<ILevelDataProvider>();
            _mapBuilder = GetComponent<IMapBuilder>();
        }

        private void Start()
        {
            var levelData = _dataProvider.GetLevel();
            var mapData = _dataProvider.GetMapData();
            _mapBuilder.BuildMap(mapData, (map)=> {"on map build".PrintColored(Color.white);});
        }

    }
    
    public interface IUserDataController
    {
        int GetUserLevel();
        void SetUserLevel();
    }
}