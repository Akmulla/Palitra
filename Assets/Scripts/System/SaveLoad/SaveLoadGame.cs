using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadGame : MonoBehaviour
{
    //int lvl = 0;
    public static SaveLoadGame save_load;

    public void SaveProgress(int lvl)
    {
        
        PlayerPrefs.SetInt("Progress", lvl);
        PlayerPrefs.Save();
       
    }

    public void Awake()
    {
        save_load = this;
        if (!PlayerPrefs.HasKey("Progress"))
        {
            PlayerPrefs.SetInt("Progress", 0);
            PlayerPrefs.Save();
        }
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

        GameController.game_controller.SetCurrentLvl(PlayerPrefs.GetInt("Progress"));
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("Progress", 0);
        PlayerPrefs.Save();
        GameController.game_controller.SetCurrentLvl(PlayerPrefs.GetInt("Progress"));
    }
}
