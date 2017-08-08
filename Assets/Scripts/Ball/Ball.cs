using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public static Ball ball;
    [HideInInspector]
    //public Transform tran;
    //BallMove ball_move;
    // public SpriteRenderer sprite_rend;
    RectTransform tran;
    //RectTransform collision_tran;
    //[HideInInspector]
    //public float size_x;
    bool shield;
    Image image;
    //public Transform collision_point;
    Color ball_color;
    public GameObject death;

    int lines_checked;

    public Vector3 GetPosition()
    {
        Vector3 pos = tran.rect.center;
        
        return Camera.main.ScreenToWorldPoint(tran.TransformPoint(pos));
    }

    public Vector3 GetCollisionPosition()
    {
        Vector3 pos = new Vector3(tran.rect.center.x,tran.rect.yMax,0.0f);

        return Camera.main.ScreenToWorldPoint(tran.TransformPoint(pos));
    }

    public Color GetColor()
    {
        //Color color=Color.black;
        //return color;
        return ball_color;
    }

    public void Stop()
    {
        BallMove.ball_move.Stop();
        //sprite_rend.gameObject.SetActive(false);
        image.enabled=false;
        Vector3 pos = GetPosition();
        pos.z = 0.0f;
        Instantiate(death, pos, Quaternion.identity);
    }

	void Awake()
    {
        lines_checked = 0;
        ball = this;
        shield = false;
        image = GetComponent<Image>();
        //tran = GetComponent<Transform>();
        tran = GetComponent<RectTransform>();
        
        SetColor(SkinManager.skin_manager.GetCurrentSkin().colors[0],true);
        // size_x = sprite_rend.sprite.bounds.extents.x * tran.localScale.x;
    }

	public void SetColor(Color color,bool tap)
    {
        if ((color!= ball_color)||(tap))
        {
            ball_color = color;
            //sprite_rend.color = color;
            image.color = color;
            EventManager.TriggerEvent("BallColorChanged");
        }
        //print(ball_color);
    }

    void OnEnable()
    {
        EventManager.StartListening("ChangeLvl", ChangeLvl);
        //if (GameController.game_controller.GetState()!=GameState.GameOver)
        //{
        //    image.enabled = true;
        //}
        image.enabled = true;
    }

    public void EnableImage()
    {
        image.enabled = true;
    }
    void OnDisable()
    {
        EventManager.StopListening("ChangeLvl", ChangeLvl);
    }

    public void LinePassed(Color line_color)
    {
        lines_checked++;
        //if (((Vector4)line_color - (Vector4)sprite_rend.color).magnitude<0.01f)
        if (line_color==ball_color)
        {
            if (lines_checked >= GameController.game_controller.GetLvlData().lines_to_accel)
            {
                BallMove.ball_move.IncreaseSpeed(GameController.game_controller.GetLvlData().accel);
                lines_checked = 0;
            }
        }
        else
        {
            //if (shield)
            //{
            //    shield = false;
            //}
            //else
            {
                GameController.game_controller.GameOver();
            }
        }
        EventManager.TriggerEvent("LinePassed");
        
    }
    public int GetLinesCheckedNumber()
    {
        return lines_checked;
    }

    public void LinePassed(List<Color> line_color,bool invert)
    {
        bool passed=false;

        foreach (Color item in line_color)
        {
            if (item ==ball_color)
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
                BallMove.ball_move.IncreaseSpeed(GameController.game_controller.GetLvlData().accel);
                lines_checked = 0;
            }
            EventManager.TriggerEvent("LinePassed");
            lines_checked++;
        }
        else
        {
            //if (shield)
            //{
            //    shield = false;
            //}
            //else
            {
                GameController.game_controller.GameOver();
            }
        }
        
    }

    void ChangeLvl()
    {
        ball_color=image.color;
        shield = true;
    }
}
