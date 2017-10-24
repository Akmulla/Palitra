﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeManager : MonoBehaviour
{
    public Text timeText;
    DateTime time;
    DateTime lastTime;
    TimeSpan remainTime;
    


    void Start ()
    {
        //time = DateTimeClass.GetNISTDate();
        time = DateTime.Now;
        timeText.text = time.Hour + ":" + time.Minute;
        //second = time.Second;
        //minute = time.Minute;
        //hour = time.Hour;
        //lastTime = time;
        lastTime = SaveLoadGame.save_load.LoadTime();
        CheckNewHearts();
        //print(lastTime+" " + lastTime.Second);
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
        
        time = time.AddSeconds(Time.deltaTime);

       // if (Hearts.h.Heart < 10)
            CheckNewHearts();

        //timeText.text = time.Hour + ":" + time.Minute;
        timeText.text = remainTime.Minutes + ":" + remainTime.Seconds;

    }
}
