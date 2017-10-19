using UnityEngine;

public class DestroyOnExit : MonoBehaviour
{
    Pool _pool;
    Transform _tran;
    float _sizeY;

    void Start()
    {
        _pool = GetComponent <PoolRef>().GetPool();
        _tran = GetComponent<Transform>();
        _sizeY = GetComponent<Line>().GetHeight();
    }

    void Update()
    {
        if (_tran.position.y + _sizeY < Edges.botEdge)
            _pool.Deactivate(gameObject);
    }
}
