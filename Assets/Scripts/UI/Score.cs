using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
	public Text text;
    //int lines_passed;

	void Start () 
	{
		text = GetComponent<Text> ();
       // lines_passed = 0;
		text.text = GlobalScore.globalScore.Score.ToString ();
	}
	
	void OnEnable()
    {
        EventManager.StartListening("LvlFinished", LvlFinished);
    }

    void OnDisable()
    {
        EventManager.StopListening("LvlFinished", LvlFinished);
    }

    void LvlFinished()
    {
        //int coeff = (GameController.game_controller.GetCurrentLvl() + 1) / 5;
        int addScore = 0;
        addScore += 10;
        if (GameController.gameController.CurrentLvl != 0)
        {
            if ((GameController.gameController.CurrentLvl + 1) % 5 == 0)
            {
                addScore += GameController.gameController.CurrentLvl + 1;
            }

        }

        if (Ball.ball.trianType == TrianType.DoublePoints)
            addScore *= 2;

        if (Ball.ball.trianType == TrianType.HalfPoints)
            addScore /= 2;



        GlobalScore.globalScore.Score += addScore;
        text.text = GlobalScore.globalScore.Score.ToString();

        //GlobalScore.global_score.Score += 10;
        //if (GameController.game_controller.CurrentLvl!=0)
        //{
        //    if ((GameController.game_controller.CurrentLvl+1)%5 == 0)
        //    {
        //        GlobalScore.global_score.Score += GameController.game_controller.CurrentLvl+1;
        //    }
        //text.text = GlobalScore.global_score.Score.ToString();
        //}


    }
}
