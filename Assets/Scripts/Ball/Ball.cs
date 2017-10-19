using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public static Ball ball;
    public bool shield;
    [HideInInspector]
    RectTransform _tran;
    Image _image;
    Color _ballColor;
    public GameObject death;
    public TrianType trianType=TrianType.Default;

    int _linesChecked;
    

    public Vector3 GetPosition()
    {
        Vector3 pos = _tran.rect.center;
        
        return Camera.main.ScreenToWorldPoint(_tran.TransformPoint(pos));
    }

    public Vector3 GetCollisionPosition()
    {
        Vector3 pos = new Vector3(_tran.rect.center.x,_tran.rect.yMax,0.0f);

        return Camera.main.ScreenToWorldPoint(_tran.TransformPoint(pos));
    }

    public Color GetColor()
    {
        return _ballColor;
    }

    public void Stop()
    {
        BallMove.ballMove.Stop();
        _image.enabled=false;
        Vector3 pos = GetPosition();
        pos.z = 0.0f;
        Instantiate(death, pos, Quaternion.identity);
    }

	void Awake()
    {
        _linesChecked = 0;
        ball = this;
        _image = GetComponent<Image>();
        _tran = GetComponent<RectTransform>();
        
        SetColor(SkinManager.skinManager.GetCurrentSkin().colors[0],true);
    }

	public void SetColor(Color color,bool tap)
    {
        if ((color!= _ballColor)||(tap))
        {
            _ballColor = color;
            _image.color = color;
            EventManager.TriggerEvent("BallColorChanged");
        }
    }

    void OnEnable()
    {
        EventManager.StartListening("ChangeLvl", ChangeLvl);
        EventManager.StartListening("BeginGame", BeginGame);
        _image.enabled = true;
        _image.sprite = TrianManager.trianManager.GetCurrentTrian().sprite;
        trianType = TrianManager.trianManager.GetCurrentTrian().trianType;
    }

    void OnDisable()
    {
        EventManager.StopListening("ChangeLvl", ChangeLvl);
        EventManager.StopListening("BeginGame", BeginGame);
    }

    public void EnableImage()
    {
        _image.enabled = true;
    }

    public bool LinePassed(Color lineColor)
    {
        //lines_checked++;
        if (lineColor==_ballColor)
        {
            //BallMove.ball_move.IncreaseSpeed(GameController.game_controller.GetLvlData().step_speed);
            ChngLvlStats();
            _linesChecked++;
            EventManager.TriggerEvent("LinePassed");
            return true;
        }
        if (shield)
        {
            shield = false;
            StartCoroutine(BallMove.ballMove.ShieldSlowDown());

            ChngLvlStats();
            _linesChecked++;
            EventManager.TriggerEvent("LinePassed");

            return true;
        }
        GameController.gameController.GameOver();
        return false;
    }

    public int GetLinesCheckedNumber()
    {
        return _linesChecked;
    }

    void ChngLvlStats()
    {
        LvlType lvlType = GameController.gameController.GetLvlData().lvlType;
        bool chngAftrHalf = ((lvlType == LvlType.SpeedDecrDistDecrHalf) ||
            (lvlType == LvlType.SpeedIncrDistIncrHalf)) ? true : false;
        bool passedHalf = (SpawnWaves.spawn.GetLineSpawnedNumber() >=
            GameController.gameController.GetLvlData().totalLineCount / 2);

        float k = (chngAftrHalf && passedHalf) ? -1.0f : 1.0f;

        BallMove.ballMove.IncreaseSpeed(GameController.gameController.GetLvlData().stepSpeed * k);
    }

    public bool LinePassed(List<Color> lineColor,bool invert)
    {
        bool passed = false;

        foreach (Color item in lineColor)
        {
            if (((Vector4)item - (Vector4)_ballColor).magnitude<0.01f)
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
            _linesChecked++;
            EventManager.TriggerEvent("LinePassed");

            return true;
        }
        if (shield)
        {
            shield = false;
            StartCoroutine(BallMove.ballMove.ShieldSlowDown());

            ChngLvlStats();
            _linesChecked++;
            EventManager.TriggerEvent("LinePassed");

            return true;
        }
        GameController.gameController.GameOver();
        return false;
    }

    void BeginGame()
    {
        if (trianType == TrianType.Shield)
        {
            shield = true;
        }
    }
    void ChangeLvl()
    {
        BallMove.ballMove.Speed = GameController.gameController.GetLvlData().speed;
        if (trianType==TrianType.Shield)
        {
            shield = true;
        }
        //ball_color = image.color;
    }

    void Update()
    {
        _ballColor = _image.color;
    }
}

