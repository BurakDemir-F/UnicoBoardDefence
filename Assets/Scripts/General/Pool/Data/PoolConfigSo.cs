﻿using System.Collections.Generic;
using UnityEngine;

namespace General.Pool.Data
{
    [CreateAssetMenu(fileName = "PoolConfig", menuName = "ScriptableData/PoolConfig", order = 0)]
    public class PoolConfigSo : ScriptableObject
    {
        public List<PoolConfig> PoolConfig;
    }
}