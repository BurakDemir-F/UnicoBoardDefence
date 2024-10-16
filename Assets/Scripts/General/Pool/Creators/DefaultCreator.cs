using General.Pool.System;
using UnityEngine;

namespace General.Pool.Creators
{
    public class DefaultCreator : PoolObjectCreator
    {
        public override IPoolObject CreatePoolBehaviour(GameObject prefab, string poolKey, IPool pool, Transform root)
        {
            var poolItem =  base.CreatePoolBehaviour(prefab, poolKey, pool, root);
            poolItem.Go.SetActive(false);
            return poolItem;
        }
    }
}