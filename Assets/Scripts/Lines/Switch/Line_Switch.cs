using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Line_Switch : Line
{
    float dist;
    Color line_color;
    public static Texture2D[] text;
    public static int text_ind;

    public static void InitText()
    {
        text_ind = 0;
        text = new Texture2D[3];
    }

    public override void ChangeColor()
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < SkinManager.skin_manager.GetCurrentSkin().colors.Length; i++)
        {
            if (SkinManager.skin_manager.GetCurrentSkin().colors[i] != line_color)
            {
                indexes.Add(i);
            }
        }
        int ind = indexes[Random.Range(0, indexes.Count)];
        Color new_color = SkinManager.skin_manager.GetCurrentSkin().colors[ind];
        line_color = new_color;

        SetTexture(text[ind]);
    }

    void ChangeColor(int ind)
    {
        Color new_color = SkinManager.skin_manager.GetCurrentSkin().colors[ind];
        line_color = new_color;
        SetTexture(text[ind]);
    }

    protected override void CheckIfPassed()
    {
        Ball.ball.LinePassed(line_color);
        anim.BeginAnimation();
    }

    public override void InitLine()
    {
        for (int i = 0; i < mesh_resize.Length; i++)
        {
            mesh_resize[i].scale();
        }
        active = true;
        dist = GameController.game_controller.GetLvlData().switch_prop.dist;
        
        if (text_ind<3)
        {
            text[text_ind] = texture_handler.CreateTexture
                (SkinManager.skin_manager.GetCurrentSkin().colors[text_ind]);
            ChangeColor(text_ind);
            text_ind++;
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
                (GameController.game_controller.GetLvlData().switch_prop.time_to_change);

            if ((active) && (tran.position.y - height - Ball.ball.GetPosition().y > dist))
            {
                ChangeColor();
            }
        }
    }
}
