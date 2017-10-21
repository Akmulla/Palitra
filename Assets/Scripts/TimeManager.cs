using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeManager : MonoBehaviour
{
    public Text timeText;
    DateTime time;
    //public float param=0.0f;
    //public float second;
    //public float minute;
    //public float hour;
    DateTime lastTime;

    //public DateTime GameTime
    //{
    //    get
    //    {
    //        return time;
    //    }
    //    set
    //    {
    //        time = value;
    //        timeText.text = time.Hour + ":" + time.Minute + ":" + time.Second;
    //    }

    //}
    // Use this for initialization
    void Start ()
    {
        //time = DateTimeClass.GetNISTDate();
        time = DateTime.Now;
        timeText.text = time.Hour + ":" + time.Minute + ":" + time.Second;
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
	// Update is called once per frame
	void Update ()
    {
       // param = 1;
        
        time = time.AddSeconds(Time.deltaTime);

        if (Hearts.h.Heart < 10)
            CheckNewHearts();

        timeText.text = time.Hour + ":" + time.Minute + ":" + time.Second;

        //param -= Time.deltaTime;



        //if (param <= 0)
        //{
        //    param = 1;
        //    second = second + 1;
        //}

        //if (second >= 60)
        //{
        //    minuta = minuta + 1;
        //    second = 0;
        //}

        //if (minuta >= 60)
        //{
        //    hours = hours + 1;
        //    minuta = 0;
        //}

        //if (hours > 23)
        //{
        //    hours = 0;
        //}

        //timeText.text = hours + ":" + minuta + ":" + second;
        //timeText.text = time.Hour + ":" + time.Minute + ":" + time.Second;
        //timeText.text = time.ToString();
    }
}
