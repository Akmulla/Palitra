using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalScore : MonoBehaviour
{
    public static GlobalScore global_score;
    public 
    int score = 0;
    public Text heartMenuScore;

    void Awake()
    {
        global_score = this;
        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.Save();
        }

        //
        PlayerPrefs.SetInt("Score", 10000);

        //
        score = PlayerPrefs.GetInt("Score");
        heartMenuScore.text = score.ToString();
    }

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value >= 0 ? value : -value;
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.Save();
            heartMenuScore.text = score.ToString();
        }
    }
}
