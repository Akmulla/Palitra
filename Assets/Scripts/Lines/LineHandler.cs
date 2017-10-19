using UnityEngine;
using System.Collections;

[System.Serializable]
public class LineHandler:ScriptableObject
{
    public PoolType pool_type;
    public Pool pool;
    public int count;
}
