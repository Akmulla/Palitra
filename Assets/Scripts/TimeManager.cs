using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeManager : MonoBehaviour
{
    public Text timeText;
    public GameObject timeObj;
    DateTime time;
    DateTime lastTime;
    TimeSpan remainTime;
    bool enabled;
    
    void Start ()
    {
        //time = DateTimeClass.GetNISTDate();
        time = DateTime.Now;
        //timeText.text = time.Hour + ":" + time.Minute;
        
        //second = time.Second;
        //minute = time.Minute;
        //hour = time.Hour;
        //lastTime = time;
        lastTime = SaveLoadGame.save_load.LoadTime();
        CheckNewHearts();
        //print(lastTime+" " + lastTime.Second);
	}

    public void EnableTimer()
    {
        enabled = true;
        timeObj.SetActive(true);
    }

    public void DisableTimer()
    {
        enabled = false;
        timeObj.SetActive(false);
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

        //timeText.text = time.Hour + ":" + time.Minute;
        //timeText.text = remainTime.Minutes + ":" + remainTime.Seconds;
        //string new_string = remainTime.Minutes.ToString("mm")+ remainTime.Seconds.ToString("ss");
        string sec = remainTime.Seconds.ToString();
        if (sec.Length < 2)
            sec = "0" + sec;

        string min = remainTime.Minutes.ToString();
        if (min.Length < 2)
            min = "0" + min;

        string new_string = min + ":" + sec;
        timeText.text = new_string;
    }
}
