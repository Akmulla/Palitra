using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
	public Text text;
    int lines_passed;

	void Start () 
	{
		text = GetComponent<Text> ();
        lines_passed = 0;
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
        if (GameController.game_controller.GetCurrentLvl()!=0)
        {
            GlobalScore.global_score.Score += 10;
            if ((GameController.game_controller.GetCurrentLvl()+1)%5 == 0)
            {
                GlobalScore.global_score.Score += GameController.game_controller.GetCurrentLvl()+1;
            }
            text.text = GlobalScore.global_score.Score.ToString();
        }
    }
}
