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
        Speed = saved_speed;
        current_state = State.normal;
    }
}
