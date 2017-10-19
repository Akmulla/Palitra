using UnityEngine;

public class LineDefault : Line
{
    Color _lineColor;
    public static Color prevColor=Color.clear;
    public static int sameColors = 1;

    public override void ChangeColor()
    {

    }

    public void InitLine(Color color)
    {
        active = true;
        ChangeColor(color);
    }

    public void ChangeColor(Color newColor)
    {
        Texture2D texture = textureHandler.CreateTexture(newColor);
        SetTexture(texture);
        _lineColor = newColor;
    }

    protected override void CheckIfPassed()
    {
        //if (Ball.ball.GetColor() != line_color)
        //{
        //    print("default break");
        //    print(this.GetInstanceID());
        //    print(active);
        //    Debug.Break();
            
        //}
        
        
        if ( Ball.ball.LinePassed(_lineColor))
            anim.BeginAnimation();
    }

    public Color GetLineColor()
    {
        return _lineColor;
    }
}
