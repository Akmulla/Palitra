using UnityEngine;
using System.Collections;

public class DestroyOnExit : MonoBehaviour
{
    Pool pool;
    Transform tran;
    float size_y;

    void Start()
    {
        pool = GetComponent < PoolRef>().GetPool();
        tran = GetComponent<Transform>();
        size_y = GetComponent<Line>().GetHeight();
    }

    void Update()
    {
        if (tran.position.y + size_y < Edges.botEdge)
            pool.Deactivate(gameObject);
    }
}
