using UnityEngine;
using System.Collections;

public class BallMove : MonoBehaviour
{
	public static BallMove ball_move;
    public enum State { normal,slowed,resuming};
    State current_state=State.normal;
    Transform tran;
    float speed=1.0f;
    float saved_speed=1.0f;
    float x = 0.2f;
    private IEnumerator coroutine;
    bool stop = false;

    public void Stop()
    {
        stop = true;
    }

    void Awake()
	{
		ball_move=this;
        tran = GetComponent<Transform>();
        stop = false;
    }

    void BeginGame()
    {
        speed = GameController.game_controller.GetLvlData().speed;
        current_state = State.normal;
        stop = false;
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
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    void Update()
    {
        if ((!stop) && (GameController.game_controller.GetState() == GameState.Game))
        {
            tran.position = tran.position +  Vector3.up * speed * Time.deltaTime;
        }
    }

    public void IncreaseSpeed(float acceleration)
    {
        if (current_state != State.normal)
            return;
        Speed += acceleration;
    }

    public IEnumerator ShieldSlowDown()
    {
        float saved = speed / 2.0f;
        Speed -= saved;
        yield return new WaitForSeconds(2.0f);
        Speed += saved;
    }

    public void SlowDown(float deceleration)
    {
        
        if (current_state!=State.normal)
        {
            StopAllCoroutines();
            Speed = saved_speed;
        }

        {
            current_state = State.slowed;
            x = (Speed - deceleration) / 5.0f;
            saved_speed = Speed;
            Speed = deceleration;
        }
    }

	public State CheckState()
	{
		return current_state;
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
        current_state = State.normal;
    }

    public void ResumeSpeed()
    {
        current_state = State.resuming;
        coroutine = SlowDownCoroutine(x);
        StartCoroutine(coroutine);
        //Speed = saved_speed;
       // current_state = State.normal;
    }
}
