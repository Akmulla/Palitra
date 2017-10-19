using System.Collections;
using UnityEngine;

public class BallMove : MonoBehaviour
{
	public static BallMove ballMove;
    public enum State { Normal,Slowed,Resuming}
    State _currentState=State.Normal;
    Transform _tran;
    float _speed=1.0f;
    float _savedSpeed=1.0f;
    float _x = 0.2f;
    private IEnumerator _coroutine;
    bool _stop;

    public void Stop()
    {
        _stop = true;
    }

    void Awake()
	{
		ballMove=this;
        _tran = GetComponent<Transform>();
        _stop = false;
    }

    void BeginGame()
    {
        _speed = GameController.gameController.GetLvlData().speed;
        _currentState = State.Normal;
        _stop = false;
    }

    void OnEnable()
    {
        EventManager.StartListening("BeginGame", BeginGame);
    }

    void OnDisable()
    {
        EventManager.StopListening("BeginGame", BeginGame);
    }

    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }

    void Update()
    {
        if ((!_stop) && (GameController.gameController.GetState() == GameState.Game))
        {
            switch (Ball.ball.trianType)
            {
                case TrianType.DoublePoints:
                    _tran.position = _tran.position + Vector3.up * _speed * Time.deltaTime*1.15f;
                    break;
                case TrianType.HalfPoints:
                    _tran.position = _tran.position + Vector3.up * _speed * Time.deltaTime * 0.75f;
                    break;
                default:
                    _tran.position = _tran.position + Vector3.up * _speed * Time.deltaTime;
                    break;
            }
            
        }
    }

    public void IncreaseSpeed(float acceleration)
    {
        if (_currentState != State.Normal)
            return;
        Speed += acceleration;
    }

    public IEnumerator ShieldSlowDown()
    {
        float saved = _speed / 2.0f;
        Speed -= saved;
        yield return new WaitForSeconds(2.0f);
        Speed += saved;
    }

    public void SlowDown(float deceleration)
    {
        
        if (_currentState!=State.Normal)
        {
            StopAllCoroutines();
            Speed = _savedSpeed;
        }

        {
            _currentState = State.Slowed;
            _x = (Speed - deceleration) / 5.0f;
            _savedSpeed = Speed;
            Speed = deceleration;
        }
    }

	public State CheckState()
	{
		return _currentState;
	}

    IEnumerator SlowDownCoroutine(float x)
    {
        for (int i=0;i<5;i++)
        {
            //if (Speed+x>=saved_speed)
            //{
            //    Speed = saved_speed;
            //    break;
            //}
            Speed += x;
            yield return new WaitForSeconds(0.2f);
        }
        _currentState = State.Normal;
    }

    public void ResumeSpeed()
    {
        _currentState = State.Resuming;
        _coroutine = SlowDownCoroutine(_x);
        StartCoroutine(_coroutine);
        //Speed = saved_speed;
       // current_state = State.normal;
    }
}
