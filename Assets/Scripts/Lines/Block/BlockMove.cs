using UnityEngine;

public class BlockMove : MonoBehaviour
{
    float _speed;
    float _halfWindowSize;

	void Start ()
    {
        _halfWindowSize = (Edges.rightEdge - Edges.leftEdge)/2.0f;
    }

	void Update ()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);

        if (transform.position.x - _halfWindowSize > Edges.rightEdge)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = Edges.leftEdge - _halfWindowSize;
            transform.position = newPosition;
        }
	}

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }
}
