using UnityEngine;
using System.Collections;

public class BlockMove : MonoBehaviour
{
    float speed;
    float half_window_size;

	void Start ()
    {
        half_window_size = (Edges.rightEdge - Edges.leftEdge)/2.0f;
    }

	void Update ()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x - half_window_size > Edges.rightEdge)
        {
            Vector3 new_position = transform.position;
            new_position.x = Edges.leftEdge - half_window_size;
            transform.position = new_position;
        }
	}

    public void SetSpeed(float new_speed)
    {
        speed = new_speed;
    }
}
