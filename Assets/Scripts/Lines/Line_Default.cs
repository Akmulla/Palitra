﻿using UnityEngine;
using System.Collections;
using System;

public class Line_Default : Line
{
    Color line_color;
    public static Color prev_color=Color.black;
    public static int same_colors = 1;

    public override void ChangeColor()
    {

    }

    public void InitLine(Color color)
    {
        active = true;
        ChangeColor(color);
    }

    public void ChangeColor(Color new_color)
    {
        Texture2D texture = texture_handler.CreateTexture(new_color);
        SetTexture(texture);
        line_color = new_color;
    }

    public override void Enable()
    {
        base.Enable();
    }

    protected override void CheckIfPassed()
    {
        Ball.ball.LinePassed(line_color);
        anim.BeginAnimation();
        active = false;
    }

    public Color GetLineColor()
    {
        return line_color;
    }
}
