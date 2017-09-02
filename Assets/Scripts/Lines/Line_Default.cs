using UnityEngine;

public class Line_Default : Line
{
    Color line_color;
    public static Color prev_color=Color.clear;
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

    protected override void CheckIfPassed()
    {
        if (Ball.ball.GetColor() != line_color)
        {
            print("default break");
            print(this.GetInstanceID());
            print(active);
            Debug.Break();
            
        }
        
        
        Ball.ball.LinePassed(line_color);
        anim.BeginAnimation();
        active = false;
    }

    public Color GetLineColor()
    {
        return line_color;
    }
}
