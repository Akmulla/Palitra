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
        EventManager.StartListening("ChangeLvl", LvlChanged);
    }

    void OnDisable()
    {
        EventManager.StopListening("ChangeLvl", LvlChanged);
    }

    void LvlChanged()
    {
        print(GameController.game_controller.GetCurrentLvl());
        if (GameController.game_controller.GetCurrentLvl()!=0)
        {
            GlobalScore.global_score.Score += 10;
            if (GameController.game_controller.GetCurrentLvl()%5 == 0)
            {
                GlobalScore.global_score.Score += GameController.game_controller.GetCurrentLvl();
            }
            text.text = GlobalScore.global_score.Score.ToString();
        }
    }
}
