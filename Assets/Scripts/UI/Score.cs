using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
	//int coins;
	Text text;

    int lines_passed;


	void Start () 
	{
		text = GetComponent<Text> ();
        lines_passed = 0;
		text.text = GlobalScore.global_score.Score.ToString ();
	}
	
	void OnEnable()
    {
        EventManager.StartListening("LinePassed", LinePassed);
    }

    void OnDisable()
    {
        EventManager.StopListening("LinePassed", LinePassed);
    }

    void LinePassed()
    {
        lines_passed++;
        if (lines_passed >= GameController.game_controller.GetLvlData().lines_to_coin)
        {
            lines_passed = 0;
            GlobalScore.global_score.Score += GameController.game_controller.GetLvlData().coins_reward;
            text.text = GlobalScore.global_score.Score.ToString();
        }
    }
}
