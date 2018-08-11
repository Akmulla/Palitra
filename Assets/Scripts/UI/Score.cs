using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
	public Text text;
    //int lines_passed;

	void Start () 
	{
		text = GetComponent<Text> ();
       // lines_passed = 0;
		text.text = GlobalScore.global_score.Score.ToString ();
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
        int add_score = 0;
        add_score += 10;
        if (GameController.game_controller.CurrentLvl != 0)
        {
            if ((GameController.game_controller.CurrentLvl + 1) % 5 == 0)
            {
                add_score += GameController.game_controller.CurrentLvl + 1;
            }

        }

        if (Ball.ball.trian_type == TrianType.DoublePoints)
            add_score *= 2;

        if (Ball.ball.trian_type == TrianType.HalfPoints)
            add_score /= 2;



        GlobalScore.global_score.Score += add_score;
        text.text = GlobalScore.global_score.Score.ToString();

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
