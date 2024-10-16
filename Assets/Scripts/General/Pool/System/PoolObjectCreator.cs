using UnityEngine;
using Utilities;

namespace General.Pool.System
{
    public abstract class PoolObjectCreator : MonoBehaviour
    {
        public virtual IPoolObject CreatePoolBehaviour(GameObject prefab, string poolKey, IPool pool,Transform root)
        {
            var poolObj = Instantiate(prefab,root);
            if (poolObj.TryGetComponent<IPoolObject>(out var component))
            {
                component.Pool = pool;
                component.Key = poolKey;
                return component;
            }

            $"{poolKey},prefab should implement IPoolObject with any component".Print();
            return default;
        }
    }
}