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

    Vector3 next_pos=Vector3.zero;

    //Rigidbody2D rb;

    public void Stop()
    {
        stop = true;
    }
    void Awake()
	{
		ball_move=this;
       // rb = GetComponent<Rigidbody2D>();
	}
    void Start()
    {
        tran = GetComponent<Transform>();
        //next_pos = tran.position;
    }
    void BeginGame()
    {
        speed = GameController.game_controller.GetLvlData().min_speed;
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
    float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            //print(value);
           // Debug.Break();
            if (current_state==State.normal)
                speed = Mathf.Clamp (value, GameController.game_controller.GetLvlData ().min_speed, GameController.game_controller.GetLvlData ().max_speed);
            else
                speed = Mathf.Clamp(value, 0.1f, GameController.game_controller.GetLvlData().max_speed);
        }
    }
    void Update()
    {
        if ((!stop) && (GameController.game_controller.GetState() == GameState.Game))
            tran.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        //rb.velocity = next_pos;
    }
    public void IncreaseSpeed(float acceleration)
    {
        if (current_state != State.normal)
            return;
        Speed += acceleration;
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
            if (Speed+x>=saved_speed)
            {
                Speed = saved_speed;
                break;
            }
            Speed += x;
            yield return new WaitForSeconds(1.0f);
        }
        current_state = State.normal;
    }

    public void ResumeSpeed()
    {
        current_state = State.resuming;
        coroutine = SlowDownCoroutine(x);
        StartCoroutine(coroutine);
    }


}
