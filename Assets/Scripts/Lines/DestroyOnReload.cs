using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnReload : MonoBehaviour
{
    Pool pool;
    Line line;

    void Start()
    {
        pool = GetComponent<PoolRef>().GetPool();
        line = GetComponent<Line>();
    }

    void ToPool()
    {
        pool.Deactivate(gameObject);
        if ((GameController.game_controller.GetState() == GameState.GameOver)&&(line.CheckIfActive()))
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
