using General.Pool.System;
using UnityEngine;

namespace General.Pool.Creators
{
    public class BulletCreator : PoolObjectCreator
    {
        public override IPoolObject CreatePoolBehaviour(GameObject prefab, string poolKey, IPool pool, Transform root)
        {
            var poolItem =  base.CreatePoolBehaviour(prefab, poolKey, pool, root);
            poolItem.Go.SetActive(false);
            poolItem.Go.transform.position = Vector3.one * -1000f;
            return poolItem;
        }
    }
}