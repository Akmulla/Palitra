using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScore : MonoBehaviour
{
    public static GlobalScore global_score;
    int score = 0;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        global_score = this;
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }
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
        }
    }
}
