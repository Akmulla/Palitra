using UnityEngine;
using System.Collections;
using System;

public class Line_Default : Line
{
    Color line_color;
    static Color prev_color=Color.black;
    static int same_colors = 1;
    
    public override void ChangeColor()
    {
        Color[] colors = SkinManager.skin_manager.GetCurrentSkin().colors;
         Color new_color=colors[UnityEngine.Random.Range(0, colors.Length)];
        if (new_color==prev_color)
        {
            same_colors++;
        }
        if (same_colors>2)
        {
            Color[] avail_col = new Color[colors.Length - 1];
            int k = 0;
            for (int i=0;i<colors.Length;i++)
            {
                if (colors[i]!= new_color)
                {
                    avail_col[k] = colors[i];
                    k++;
                }
            }
            new_color = avail_col[UnityEngine.Random.Range(0, avail_col.Length)];
            same_colors = 1;
        }
        // Texture2D[] texture = TextureHandler.CreateTexture(new_color);
        Texture2D texture = TextureHandler.CreateTexture(new_color);
        SetTexture(texture);
        line_color = new_color;
        prev_color = line_color;
    }

    public override void InitLine()
    {
        active = true;
        ChangeColor();
    }

    protected override void CheckIfPassed()
    {
        Ball.ball.LinePassed(line_color);
        anim.BeginAnimation();
        active = false;
    }
}
