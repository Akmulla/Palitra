using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Line_Switch : Line
{
    float dist;
    //float saved_dist;
    Color line_color;


    public override void ChangeColor()
    {
        List<Color> colors = new List<Color>();
        for (int i = 0; i < SkinManager.skin_manager.GetCurrentSkin().colors.Length; i++)
        {
            if (SkinManager.skin_manager.GetCurrentSkin().colors[i] != line_color)
            {
                colors.Add(SkinManager.skin_manager.GetCurrentSkin().colors[i]);
            }
        }
        
        Color new_color = colors[UnityEngine.Random.Range(0, colors.Count)];
        line_color = new_color;
        //Texture2D[] texture=TextureHandler.CreateTexture(new_color);
        Texture2D texture = texture_handler.CreateTexture(new_color);
        SetTexture(texture);

    }
    public override void InitLine()
    {
        active = true;
        //saved_dist = SpawnWaves.spawn.dist;
        dist = GameController.game_controller.GetLvlData().switch_prop.dist;
        ChangeColor();
       // StartCoroutine(SwitchColor());
    }
    protected override void CheckIfPassed()
    {

        if ((active) && (tran.position.y - height <= Ball.ball.GetCollisionPosition().y))
        {
            // Debug.DrawLine(new Vector3(1.0f, tran.position.y - height, 0.0f),
            //new Vector3(1.0f, tran.position.y + height, 0.0f), Color.black, 10.0f);
            anim.BeginAnimation();
            Ball.ball.LinePassed(line_color);
        }
    }
    protected override void OnEnable()
    {
        StartCoroutine(SwitchColor());
    }
    IEnumerator SwitchColor()
    {
        //print("dfh");
        //yield return new WaitForSeconds(0.2f);
        while (gameObject.activeSelf)
        {
            //print((active) && (tran.position.y - height - Ball.ball.GetPosition().y > dist));
            //if ((active) && (tran.position.y - height - Ball.ball.GetPosition().y > saved_dist))
            if ((active) && (tran.position.y - height - Ball.ball.GetPosition().y > dist))
            {
                //line.ChangeColor(SkinManager.skin_manager.GetCurrentSkin().colors[Random.Range(0, SkinManager.skin_manager.GetCurrentSkin().colors.Length)]);
                ChangeColor();
                //print("dfh");
            }
            yield return new WaitForSeconds(GameController.game_controller.GetLvlData().switch_prop.time_to_change);
        }
        
    }

}
