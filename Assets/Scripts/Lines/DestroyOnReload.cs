using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnReload : MonoBehaviour
{
    Pool pool;

    void Start()
    {
        pool = GetComponent<PoolRef>().GetPool();
    }
    void ToPool()
    {
        pool.Deactivate(gameObject);
    }

    void OnEnable()
    {
        EventManager.StartListening("EndGame", ToPool);
    }

    void OnDisable()
    {
        EventManager.StopListening("EndGame", ToPool);
    }
}
