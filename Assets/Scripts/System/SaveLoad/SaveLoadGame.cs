using System;
using UnityEngine;

public class SaveLoadGame : MonoBehaviour
{
    //int lvl = 0;
    public int hearts;
    public bool startCustomLvl;
    public int startLvl;
    public static SaveLoadGame saveLoad;

    public void SaveProgress(int lvl)
    {
        
        PlayerPrefs.SetInt("Progress", lvl);
        PlayerPrefs.Save();
       
    }

    public void Awake()
    {
        
        saveLoad = this;
        if (startCustomLvl)
        {
            // PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("Progress", startLvl);
            PlayerPrefs.Save();
        }
            
        if (!PlayerPrefs.HasKey("Progress"))
        {
            PlayerPrefs.SetInt("Progress", 0);
            //PlayerPrefs.SetInt("Progress", 3);
        }
        PlayerPrefs.Save();
        //PlayerPrefs.DeleteAll();
        ////
        //ResetProgress();
        ////
    }

    public void LoadProgress()
    {
        if (!PlayerPrefs.HasKey("Progress"))
        {
            PlayerPrefs.SetInt("Progress", 0);
            PlayerPrefs.Save();
        }

        // GameController.game_controller.SetCurrentLvl(PlayerPrefs.GetInt("Progress"));
        GameController.gameController.CurrentLvl=PlayerPrefs.GetInt("Progress");
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("Progress", 0);
        PlayerPrefs.Save();
        //GameController.game_controller.SetCurrentLvl(PlayerPrefs.GetInt("Progress"));
        GameController.gameController.CurrentLvl=PlayerPrefs.GetInt("Progress");
    }

    public void SaveTime(DateTime time)
    {
        PlayerPrefs.SetString("Time", time.ToString());
        PlayerPrefs.Save();
    }

    public DateTime LoadTime()
    {
        DateTime time = new DateTime();
        if (!PlayerPrefs.HasKey("Time"))
        {
            time = DateTime.Now;
            SaveTime(time);
        }
        time = DateTime.Parse(PlayerPrefs.GetString("Time"));

        return time;
    }
}
