using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeManager : MonoBehaviour
{
    public Text[] timeText;
    public GameObject[] timeObj;
    //public Text timeText;
    //public Text timeText2;
    //public GameObject timeObj;
    //public GameObject timeObj2;
    DateTime time;
    DateTime lastTime;
    TimeSpan remainTime;
    bool enabled;
    
    void Start ()
    {
        time = DateTime.Now;
        lastTime = SaveLoadGame.save_load.LoadTime();
        CheckNewHearts();
	}

    public void EnableTimer()
    {
        if (!enabled)
            lastTime = time;

        enabled = true;
        
        for (int i = 0; i < timeObj.Length;i++)
        {
            timeObj[i].SetActive(true);
        }
        
    }

    public void DisableTimer()
    {
        enabled = false;
        for (int i = 0; i < timeObj.Length; i++)
        {
            timeObj[i].SetActive(false);
        }
    }
   
	void CheckNewHearts()
    {
        TimeSpan delta = time - lastTime;
        remainTime=new TimeSpan(0,5,0) - delta;
        if (delta.TotalMinutes >= 5)
        {
            lastTime = time;
            int add_hearts = (int)delta.TotalSeconds / 5;
            int new_hearts = Hearts.h.Heart + add_hearts;
            new_hearts = Mathf.Clamp(new_hearts, 0, 10);
            SaveLoadGame.save_load.SaveTime(lastTime);
            Hearts.h.Heart = new_hearts;
        }
    }
	
	void Update ()
    {
       // param = 1;
        if (!enabled)
            return;

        time = time.AddSeconds(Time.deltaTime);
        CheckNewHearts();
        
        string sec = remainTime.Seconds.ToString();
        if (sec.Length < 2)
            sec = "0" + sec;

        string min = remainTime.Minutes.ToString();
        if (min.Length < 2)
            min = "0" + min;

        string new_string = min + ":" + sec;

        for (int i = 0; i < timeText.Length; i++)
        {
            timeText[i].text = new_string;
        }
    }
}
