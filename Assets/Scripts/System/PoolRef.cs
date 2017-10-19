using UnityEngine;
using System.Collections;

public class PoolRef : MonoBehaviour
{
    Pool pool;

    public Pool GetPool()
    {
        return pool;
    }

    public void SetPool(Pool new_pool)
    {
        pool = new_pool;
    }
}
