using UnityEngine;

public class LineMultiple : Line
{
    MultipleBlockManager _blockManager;
    public int lineSpawnNumber;
    public float prevEdge;
    bool _crossed;
    public bool finished;

    public override void InitLine()
    {
        _blockManager = GetComponent<MultipleBlockManager>();
        lineSpawnNumber = SpawnWaves.spawn.GetLineSpawnedNumber();
        _crossed = false;
        finished = false;
        base.InitLine();
    }

	protected override void CheckIfCrossed ()
	{
		base.CheckIfCrossed ();
        PoolType poolType = GetComponent<PoolRef>().GetPool().poolType;
        float deceleration = 0.0f;
        switch (poolType)
        {
            case (PoolType.Multiple1Part):
                deceleration = GameController.gameController.GetLvlData().multipleProp1Part.slowing;
                break;
            case (PoolType.Multiple2Parts):
                deceleration = GameController.gameController.GetLvlData().multipleProp2Parts.slowing;
                break;
            case (PoolType.Multiple3Parts):
                deceleration = GameController.gameController.GetLvlData().multipleProp3Parts.slowing;
                break;
        }

        if ((active) && (Ball.ball.GetPosition().y > prevEdge) && (!_crossed))
        {
            BallMove.ballMove.SlowDown(deceleration);
            _crossed = true;
        }
    }

    protected override void CheckIfPassed()
    {
        bool passed;
        if (!finished)
        {
            //print("Check");
            //Debug.Break();
            passed=Ball.ball.LinePassed(Color.black);
            //GameController.game_controller.GameOver();
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

    public override void ChangeColor()
    {
        _blockManager.InitBlocks();
    }

    public override void Enable()
    {
        base.Enable();
        finished = false;
        prevEdge = SpawnWaves.spawn.prevEdge;
        lineSpawnNumber = SpawnWaves.spawn.GetLineSpawnedNumber();
        _crossed = false;
        _blockManager.SetDefault();
    }
}
