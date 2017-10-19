using UnityEngine;

public class DestroyOnReload : MonoBehaviour
{
    Pool _pool;
    Line _line;

    void Start()
    {
        _pool = GetComponent<PoolRef>().GetPool();
        _line = GetComponent<Line>();
    }

    void ToPool()
    {
        _pool.Deactivate(gameObject);
        if ((GameController.gameController.GetState() == GameState.GameOver)&&(_line.CheckIfActive()))
        {
            EventManager.TriggerEvent("LinePassed");
        }

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
