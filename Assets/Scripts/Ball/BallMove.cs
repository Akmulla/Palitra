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
    float pixel_size;

    Rigidbody rb;
    Vector3 movement = Vector3.zero;

    public void Stop()
    {
        stop = true;
    }
    void Awake()
	{
		ball_move=this;
        rb = GetComponent<Rigidbody>();
	}
    void Start()
    {
        tran = GetComponent<Transform>();
        pixel_size = (Edges.topEdge-Edges.botEdge)/Screen.height;
        //StartCoroutine(MoveCor());
        next_pos = tran.position;
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
            //print(value);
            // Debug.Break();
            //if (current_state==State.normal)
            //    speed = Mathf.Clamp (value, GameController.game_controller.GetLvlData ().min_speed, GameController.game_controller.GetLvlData ().max_speed);
            //else
            //    speed = Mathf.Clamp(value, 0.1f, GameController.game_controller.GetLvlData().max_speed);
            speed = value;
        }
    }
    void Update()
    {
        /*
        if ((!stop) && (GameController.game_controller.GetState() == GameState.Game))
            //rb.velocity = Vector2.up * speed;
            movement = Vector3.up;
        else
            movement = Vector3.zero;
            //rb.velocity = Vector2.zero;
            */
        //print(rb.velocity);
        if ((!stop) && (GameController.game_controller.GetState() == GameState.Game))
        {
            tran.position = tran.position +  Vector3.up * speed * Time.deltaTime;
        }
            //tran.Translate(Vector2.up * speed * Time.deltaTime);

        //tran.position = next_pos;
    }

    IEnumerator MoveCor()
    {
        while(true)
        {
            //print(Time.time+" "+tran.position.y);
            if ((!stop) && (GameController.game_controller.GetState() == GameState.Game))
                //tran.position+=Vector3.up * pixel_size;
                next_pos += Vector3.up * (pixel_size * 2);
            yield return new WaitForSeconds(0.002f);
        }
    }
    /*
    void FixedUpdate()
    {
        //print(tran.position.y);
    //    rb.velocity = movement*speed;
        //tran.position = next_pos;
        //if ((!stop) && (GameController.game_controller.GetState() == GameState.Game))
        //    next_pos += Vector3.up * (pixel_size * speed);

    }
    */
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
        //coroutine = SlowDownCoroutine(x);
        //StartCoroutine(coroutine);
        Speed = saved_speed;
        current_state = State.normal;
    }


}
