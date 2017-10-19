using UnityEngine;
using UnityEngine.UI;

public class GlobalScore : MonoBehaviour
{
    public static GlobalScore globalScore;
    public 
    int score;
    public Text heartMenuScore;

    void Awake()
    {
        globalScore = this;
        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.Save();
        }
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
