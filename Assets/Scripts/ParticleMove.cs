using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    Transform _cam;
    float _offset;
    Transform _tran;

	void Awake ()
    {
        _cam = Camera.main.gameObject.transform;
        _tran = GetComponent<Transform>();
        _offset = _tran.position.y - _cam.position.y;
	}
	
	void Update ()
    {
        _tran.position = new Vector3(_cam.position.x, _cam.position.y+_offset,1.0f);
	}
}
