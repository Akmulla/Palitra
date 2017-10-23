using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveLoadGame : MonoBehaviour
{
    //int lvl = 0;
    public int hearts;
    public bool StartCustomLvl = false;
    public int start_lvl;
    public static SaveLoadGame save_load;

    public void SaveProgress(int lvl)
    {
        
        PlayerPrefs.SetInt("Progress", lvl);
        PlayerPrefs.Save();
       
    }

    public void SaveHearts()
    {
        PlayerPrefs.SetInt("Hearts", Hearts.h.Heart);
        PlayerPrefs.Save();

        //print(PlayerPrefs.GetInt("Hearts"));
    }


    public int LoadHearts()
    {
        //print("load func");
        if (!PlayerPrefs.HasKey("Hearts"))
        {
            PlayerPrefs.SetInt("Hearts", 10);
            PlayerPrefs.Save();
            //print("created new");
        }
#if UNITY_EDITOR
        PlayerPrefs.SetInt("Hearts", 100);
#endif
        // GameController.game_controller.SetCurrentLvl(PlayerPrefs.GetInt("Progress"));
        return PlayerPrefs.GetInt("Hearts");
        //return 50;
    }

    public void Awake()
    {
        
        save_load = this;
        //PlayerPrefs.DeleteAll();
        if (StartCustomLvl)
        {
            // PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("Progress", start_lvl);
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
        //
#if UNITY_EDITOR
        PlayerPrefs.SetInt("Progress", 10000);
#endif
        //PlayerPrefs.SetInt("Progress", 10000);
        //
        // GameController.game_controller.SetCurrentLvl(PlayerPrefs.GetInt("Progress"));
        GameController.game_controller.CurrentLvl=PlayerPrefs.GetInt("Progress");
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("Progress", 0);
        PlayerPrefs.Save();
        //GameController.game_controller.SetCurrentLvl(PlayerPrefs.GetInt("Progress"));
        GameController.game_controller.CurrentLvl=PlayerPrefs.GetInt("Progress");
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
