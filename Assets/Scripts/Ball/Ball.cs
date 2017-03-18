﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public static Ball ball;
    [HideInInspector]
    public Transform tran;
    BallMove ball_move;
    public SpriteRenderer sprite_rend;
    [HideInInspector]
    public float size_x;
    bool shield;

    int lines_checked;

	void Awake()
    {
        lines_checked = 0;
        ball = this;
        shield = false;
        ball_move = GetComponent<BallMove>();
        tran = GetComponent<Transform>();
        SetColor(GameController.game_controller.GetLvlData().colors[0]);
        size_x = sprite_rend.sprite.bounds.extents.x * tran.localScale.x;
    }

    void Start()
    {
        
    }

	public void SetColor(Color color)
    {
        sprite_rend.color = color;
        EventManager.TriggerEvent("BallColorChanged");
    }

    void OnEnable()
    {
        EventManager.StartListening("ChangeLvl", ChangeLvl);
    }
    void OnDisable()
    {
        EventManager.StopListening("ChangeLvl", ChangeLvl);
    }

    public void LinePassed(Color line_color)
    {
        

        if (line_color == sprite_rend.color)
        {
            if (lines_checked >= GameController.game_controller.GetLvlData().lines_to_accel)
            {
                ball_move.IncreaseSpeed(GameController.game_controller.GetLvlData().accel);
                lines_checked = 0;
            }
        }
        else
        {
            //print(line_color);
            //print(sprite_rend.color);
            if (shield)
            {
                shield = false;
            }
            else
            {
                GameController.game_controller.GameOver();
            }
        }
        EventManager.TriggerEvent("LinePassed");
        lines_checked++;
    }


    public void LinePassed(List<Color> line_color,bool invert)
    {

        
        bool passed=false;

        foreach (Color item in line_color)
        {
            if (item == sprite_rend.color)
            {
                passed = true;
                break;
            }
        }

        if (invert)
            passed = !passed;

        if (passed)
        {
            if (lines_checked >= GameController.game_controller.GetLvlData().lines_to_accel)
            {
                ball_move.IncreaseSpeed(GameController.game_controller.GetLvlData().accel);
                lines_checked = 0;
            }
        }
        else
        {
            //print(line_color);
            //print(sprite_rend.color);
            if (shield)
            {
                shield = false;
            }
            else
            {
                GameController.game_controller.GameOver();
            }
        }
        EventManager.TriggerEvent("LinePassed");
        lines_checked++;
    }

    void ChangeLvl()
    {
        shield = true;
        //SetColor(GameController.game_controller.GetLvlData().colors[0]);
    }


    

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Line"))
    //    {
    //        EventManager.TriggerEvent("LinePassed");

    //        if (sprite_rend.color == other.GetComponent<SpriteRenderer>().color)
    //        {
    //            lines_checked++;
    //            if (lines_checked >= GameController.game_controller.GetLvlData().lines_to_accel)
    //            {
    //                ball_move.IncreaseSpeed(GameController.game_controller.GetLvlData().accel);
    //                lines_checked = 0;
    //            }
    //        }
    //        else
    //        {
    //            if (shield)
    //            {
    //                shield = false;
    //            }
    //            else
    //            {
    //                GameController.game_controller.GameOver();
    //            }
    //        }
    //    }
    //}
}
