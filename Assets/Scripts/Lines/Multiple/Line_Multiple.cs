using UnityEngine;
using System.Collections;

public class Line_Multiple : Line
{
    Multiple_BlockManager block_manager;
    public int line_spawn_number;
    public float prev_edge;
    bool crossed=false;
    public bool finished = false;

    public override void InitLine()
    {
        block_manager = GetComponent<Multiple_BlockManager>();
        line_spawn_number = SpawnWaves.spawn.GetLineSpawnedNumber();
        crossed = false;
        finished = false;
        base.InitLine();
    }

	protected override void CheckIfCrossed ()
	{
		base.CheckIfCrossed ();
        PoolType pool_type = GetComponent<PoolRef>().GetPool().pool_type;
        float deceleration=0.0f;
        switch (pool_type)
        {
            case (PoolType.Multiple_1_part):
                deceleration = GameController.game_controller.GetLvlData().multiple_prop_1_part.slowing;
                break;
            case (PoolType.Multiple_2_parts):
                deceleration = GameController.game_controller.GetLvlData().multiple_prop_2_parts.slowing;
                break;
            case (PoolType.Multiple_3_parts):
                deceleration = GameController.game_controller.GetLvlData().multiple_prop_3_parts.slowing;
                break;
        }

        if ((active) && (Ball.ball.GetPosition().y > prev_edge) && (!crossed))
        {
            BallMove.ball_move.SlowDown(deceleration);
            crossed = true;
        }
    }

    protected override void CheckIfPassed()
    {
        if (!finished)
        {
            Ball.ball.LinePassed(Color.black);
        }
        else
        {
            anim.BeginAnimation();
            active = false;
            Ball.ball.LinePassed(Ball.ball.GetColor());
        }
    }

    public override void ChangeColor()
    {
        block_manager.InitBlocks();
    }

    public override void Enable()
    {
        base.Enable();
        finished = false;
        prev_edge = SpawnWaves.spawn.prev_edge;
        line_spawn_number = SpawnWaves.spawn.GetLineSpawnedNumber();
        crossed = false;
        block_manager.SetDefault();
    }
}
