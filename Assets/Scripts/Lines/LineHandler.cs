using System;
using UnityEngine;

[Serializable]
public class LineHandler:ScriptableObject
{
    public PoolType poolType;
    public Pool pool;
    public int count;
}
