using UnityEngine;

public class PoolRef : MonoBehaviour
{
    Pool _pool;

    public Pool GetPool()
    {
        return _pool;
    }

    public void SetPool(Pool newPool)
    {
        _pool = newPool;
    }
}
