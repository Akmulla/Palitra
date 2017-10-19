using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSwitch : Line
{
    float _dist;
    Color _lineColor;
    public static Texture2D[] text;
    public static int textInd;

    public static void InitText()
    {
        textInd = 0;
        text = new Texture2D[3];
    }

    public override void ChangeColor()
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < SkinManager.skinManager.GetCurrentSkin().colors.Length; i++)
        {
            if (SkinManager.skinManager.GetCurrentSkin().colors[i] != _lineColor)
            {
                indexes.Add(i);
            }
        }
        int ind = indexes[Random.Range(0, indexes.Count)];
        Color newColor = SkinManager.skinManager.GetCurrentSkin().colors[ind];
        _lineColor = newColor;

        SetTexture(text[ind]);
    }

    void ChangeColor(int ind)
    {
        Color newColor = SkinManager.skinManager.GetCurrentSkin().colors[ind];
        _lineColor = newColor;
        SetTexture(text[ind]);
    }

    protected override void CheckIfPassed()
    {
        if (Ball.ball.LinePassed(_lineColor))
            anim.BeginAnimation();
    }

    public override void InitLine()
    {
        for (int i = 0; i < meshResize.Length; i++)
        {
            meshResize[i].Scale();
        }
        active = true;
        _dist = GameController.gameController.GetLvlData().switchProp.dist;
        
        if (textInd<3)
        {
            text[textInd] = textureHandler.CreateTexture
                (SkinManager.skinManager.GetCurrentSkin().colors[textInd]);
            ChangeColor(textInd);
            textInd++;
        }
        else
        {
            ChangeColor();
        }
    }

    protected override void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(SwitchColor());
    }

    IEnumerator SwitchColor()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds
                (GameController.gameController.GetLvlData().switchProp.timeToChange);

            if ((active) && (tran.position.y - height - Ball.ball.GetPosition().y > _dist))
            {
                ChangeColor();
            }
        }
    }
}
