using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timeText;
    DateTime _time;
    public float param;
    public float second;
    public float minute;
    public float hour;
    DateTime _lastTime;

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
        _time = DateTime.Now;
        timeText.text = _time.Hour + ":" + _time.Minute + ":" + _time.Second;
        second = _time.Second;
        minute = _time.Minute;
        hour = _time.Hour;
        //lastTime = time;
        _lastTime = SaveLoadGame.saveLoad.LoadTime();
        CheckNewHearts();
        //print(lastTime+" " + lastTime.Second);
	}

   
	void CheckNewHearts()
    {
        TimeSpan delta = _time - _lastTime;
        if (delta.TotalSeconds > 500)
        {
            _lastTime = _time;
            Hearts.h.Heart += (int)delta.TotalSeconds / 5;
        }
    }
	// Update is called once per frame
	void Update ()
    {
       // param = 1;
        
        _time=_time.AddSeconds(Time.deltaTime);

        CheckNewHearts();
        timeText.text = _time.Hour + ":" + _time.Minute + ":" + _time.Second;

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
