using UnityEngine;

public class LineOrder : Line
{
    BlockManagerOrder _blockManager;
    bool _crossed;
    public int lineSpawnNumber;
    public float prevEdge;
    public bool finished;

    public override void InitLine()
    {
        _blockManager = GetComponent<BlockManagerOrder>();
        base.InitLine();
        lineSpawnNumber = SpawnWaves.spawn.GetLineSpawnedNumber();
        _crossed = false;
        finished = false;
        //prev_edge = SpawnWaves.spawn.prev_edge;
    }

    public override void Enable()
    {
        base.Enable();
        prevEdge = SpawnWaves.spawn.prevEdge;
        _crossed = false;
        finished = false;
        _blockManager.SetDefault();
    }

    protected override void CheckIfPassed()
    {
        bool passed;
        if (!finished)
        {
            passed=Ball.ball.LinePassed(Color.black);
        }
        else
        {
            passed=Ball.ball.LinePassed(Ball.ball.GetColor());
        }

        if (passed)
        {
            anim.BeginAnimation();
        }
        //active = false;
    }

    protected override void CheckIfCrossed()
    {
        base.CheckIfCrossed();

        float deceleration = 0.0f;
        PoolType poolType = GetComponent<PoolRef>().GetPool().poolType;
        switch (poolType)
        {
            case (PoolType.Combo3Parts):
                deceleration = GameController.gameController.GetLvlData().comboProp3Parts.slowing;
                break;
            case (PoolType.Combo4Parts):
                deceleration = GameController.gameController.GetLvlData().comboProp4Parts.slowing;
                break;
            case (PoolType.Combo5Parts):
                deceleration = GameController.gameController.GetLvlData().comboProp5Parts.slowing;
                break;
        }
        if ((active) && (Ball.ball.GetPosition().y > prevEdge) && (!_crossed))
        {
            BallMove.ballMove.SlowDown(deceleration);
            EventManager.TriggerEvent("BallColorChanged");
            _crossed = true;
        }
    }

    public override void ChangeColor()
    {
        _blockManager.InitBlocks();
    }
}
