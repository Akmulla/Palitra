﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Line_Switch : Line
{
    public float dist;
    //Color line_color;


    public override void ChangeColor()
    {
        
            List<Color> colors = new List<Color>();
            for (int i = 0; i < GameController.game_controller.GetLvlData().colors.Length; i++)
            {
                if (GameController.game_controller.GetLvlData().colors[i] != base.sprite_rend.color)
                {
                    colors.Add(GameController.game_controller.GetLvlData().colors[i]);
                }
            }

        base.sprite_rend.color=colors[UnityEngine.Random.Range(0, colors.Count)];

        //Color[] colors = GameController.game_controller.GetLvlData().colors;
        //Color new_color = colors[UnityEngine.Random.Range(0, colors.Length)];
        //line_color = new_color;
        //base.sprite_rend.color = new_color;
    }
    protected override void InitLine()
    {
        
    }
    protected override void CheckIfPassed()
    {
        
       Ball.ball.LinePassed(base.sprite_rend.color);
        
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(SwitchColor());
    }
    IEnumerator SwitchColor()
    {
        //print("dfh");
        //yield return new WaitForSeconds(0.2f);
        while (gameObject.activeSelf)
        {
            //print((active) && (tran.position.y - height - Ball.ball.tran.position.y > dist));
            if ((active) && (tran.position.y - height - Ball.ball.tran.position.y > dist))
            {
                //line.ChangeColor(GameController.game_controller.GetLvlData().colors[Random.Range(0, GameController.game_controller.GetLvlData().colors.Length)]);
                ChangeColor();
                //print("dfh");
            }
            yield return new WaitForSeconds(GameController.game_controller.GetLvlData().switch_prop.time_to_change);
        }
        
    }

}
