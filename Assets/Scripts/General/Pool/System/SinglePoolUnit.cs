using System.Collections.Generic;
using General.Pool.Data;
using UnityEngine;
using Utilities;

namespace General.Pool.System
{
    public class SinglePoolUnit : IPool
    {
        private readonly Stack<IPoolObject> _objectStack;
        private readonly PoolConfig _config;
        private readonly IPoolCollection _rootPoolCollection;
        private readonly Transform _root;
        public SinglePoolUnit(PoolConfig config, IPoolCollection rootPoolCollection,Transform root)
        {
            _rootPoolCollection = rootPoolCollection;
            _config = config;
            _root = root;
            var defaultCapacity = config.DefaultCapacity;
            _objectStack = new Stack<IPoolObject>(defaultCapacity);
            for (int i = 0; i < defaultCapacity; i++)
            {
                _objectStack.Push(CreateNew());
            }
        }
        
        public IPoolObject Get()
        {
            if (_objectStack.Count > 0)
            {
                var poolObj =  _objectStack.Pop();
                poolObj.OnGetFromPool();
                return poolObj;
            }

            var newPoolObj = CreateNew();
            newPoolObj.OnGetFromPool();
            return newPoolObj;
        }
        public T Get<T>() where T : IPoolObject
        {
            return (T)Get();
        }
        
        public void Return(IPoolObject poolObject)
        {
            if (poolObject.Pool != this)
            {
                "something wrong here!".Print();
                return;
            }
            poolObject.OnReturnedToPool();
            poolObject.Go.transform.SetParent(_root);
            _objectStack.Push(poolObject);
        }

        private IPoolObject CreateNew()
        {
            var newPoolObj = _config.Creator.CreatePoolBehaviour(_config.PoolObjectPrefab,_config.PoolKey,this,_root);
            newPoolObj.Pool = this;
            newPoolObj.Key = _config.PoolKey;
            return newPoolObj;
        }
    }
}