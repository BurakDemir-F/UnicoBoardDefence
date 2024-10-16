﻿using System.Collections.Generic;
using General.Pool.Data;
using UnityEngine;

namespace General.Pool.System
{
    
    public class MasterPool : MonoBehaviour, IPoolCollection
    {
        [SerializeField] private PoolConfigSo _config;
        private Dictionary<string, IPool> _poolDict;
        [SerializeField]private bool _isInitialized;

        public void InitializePool(List<PoolConfig> config,Transform transform)
        {
            _poolDict = new Dictionary<string, IPool>();
            foreach (var poolConfig in config)
                _poolDict.Add(poolConfig.PoolKey, new SinglePoolUnit(poolConfig, this,transform));
        }

        public IPoolObject Get(string key)
        {
            return _poolDict[key].Get();
        }

        public T Get<T>(string key) where T : IPoolObject
        {
            return _poolDict[key].Get<T>();
        }

        public void Return(IPoolObject poolObj)
        {
            _poolDict[poolObj.Key].Return(poolObj);
        }
        
        public IPool GetPool(string key)
        {
            return _poolDict[key];
        }

        public void CheckAndInitialize(Transform transform)
        {
            if (_isInitialized) return;
            
            InitializePool(_config.PoolConfig,transform);
            _isInitialized = true;
        }
    }
}