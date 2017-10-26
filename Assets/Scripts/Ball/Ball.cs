using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public static Ball ball;
    public bool shield = false;
    [HideInInspector]
    RectTransform tran;
    Image image;
    Color ball_color;
    public GameObject death;
    public GameObject stroke;
    public TrianType trian_type=TrianType.Default;

    int lines_checked;

    public bool Shield
    {
        get
        {
            return shield;
        }
        set
        {
            shield = value;
            stroke.SetActive(shield);
        }
    }
    

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
        return ball_color;
    }

    public void Stop()
    {
        BallMove.ball_move.Stop();
        image.enabled=false;
        Vector3 pos = GetPosition();
        pos.z = 0.0f;
        Instantiate(death, pos, Quaternion.identity);
    }

	void Awake()
    {
        lines_checked = 0;
        ball = this;
        image = GetComponent<Image>();
        tran = GetComponent<RectTransform>();
        
        SetColor(SkinManager.skin_manager.GetCurrentSkin().colors[0],true);
    }

	public void SetColor(Color color,bool tap)
    {
        if ((color!= ball_color)||(tap))
        {
            ball_color = color;
            image.color = color;
            EventManager.TriggerEvent("BallColorChanged");
        }
    }

    void OnEnable()
    {
        EventManager.StartListening("ChangeLvl", ChangeLvl);
        EventManager.StartListening("BeginGame", BeginGame);
        image.enabled = true;
        image.sprite = TrianManager.trian_manager.GetCurrentTrian().sprite;
        trian_type = TrianManager.trian_manager.GetCurrentTrian().trian_type;
        Shield = trian_type == TrianType.Shield;


    }

    void OnDisable()
    {
        EventManager.StopListening("ChangeLvl", ChangeLvl);
        EventManager.StopListening("BeginGame", BeginGame);
    }

    public void EnableImage()
    {
        image.enabled = true;
    }

    public bool LinePassed(Color line_color)
    {
        //lines_checked++;
        if (line_color==ball_color)
        {
            //BallMove.ball_move.IncreaseSpeed(GameController.game_controller.GetLvlData().step_speed);
            ChngLvlStats();
            lines_checked++;
            EventManager.TriggerEvent("LinePassed");
            return true;
        }
        else
        {
            if (Shield)
            {
                Shield = false;
                StartCoroutine(BallMove.ball_move.ShieldSlowDown());

                ChngLvlStats();
                lines_checked++;
                EventManager.TriggerEvent("LinePassed");

                return true;
            }
            else
            {
                GameController.game_controller.GameOver();
                return false;
            }
        }
    }

    public int GetLinesCheckedNumber()
    {
        return lines_checked;
    }

    void ChngLvlStats()
    {
        LvlType lvl_type = GameController.game_controller.GetLvlData().lvl_type;
        bool chng_aftr_half = ((lvl_type == LvlType.Speed_decr_dist_decr_half) ||
            (lvl_type == LvlType.Speed_incr_dist_incr_half)) ? true : false;
        bool passed_half = (SpawnWaves.spawn.GetLineSpawnedNumber() >=
            GameController.game_controller.GetLvlData().total_line_count / 2);

        float k = (chng_aftr_half && passed_half) ? -1.0f : 1.0f;

        BallMove.ball_move.IncreaseSpeed(GameController.game_controller.GetLvlData().step_speed * k);
    }

    public bool LinePassed(List<Color> line_color,bool invert)
    {
        bool passed = false;

        foreach (Color item in line_color)
        {
            if (((Vector4)item - (Vector4)ball_color).magnitude<0.01f)
            {
                passed = true;
                break;
            }
        }

        if (invert)
            passed = !passed;

        if (passed)
        {
            ChngLvlStats();
            lines_checked++;
            EventManager.TriggerEvent("LinePassed");

            return true;
        }
        else
        {
            if (Shield)
            {
                Shield = false;
                StartCoroutine(BallMove.ball_move.ShieldSlowDown());

                ChngLvlStats();
                lines_checked++;
                EventManager.TriggerEvent("LinePassed");

                return true;
            }
            else
            {
                GameController.game_controller.GameOver();
                return false;
            }
            
        }
        
    }

    void BeginGame()
    {
        if (trian_type == TrianType.Shield)
        {
            Shield = true;
        }
    }
    void ChangeLvl()
    {
        BallMove.ball_move.Speed = GameController.game_controller.GetLvlData().speed;
        if (trian_type==TrianType.Shield)
        {
            Shield = true;
        }
        //ball_color = image.color;
    }

    void Update()
    {
        ball_color = image.color;
    }
}

