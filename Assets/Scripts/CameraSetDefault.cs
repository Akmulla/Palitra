using UnityEngine;

public class CameraSetDefault : MonoBehaviour
{
    Transform _tran;
    Vector3 _defaultPosition = new Vector3(0.0f, 0.0f, -10.0f);

    void Start()
    {
        _tran = GetComponent<Transform>();
    }

    void SetDefaultPosition()
    {
        _tran.position = _defaultPosition;
    }

    void OnEnable()
    {
        EventManager.StartListening("EndGame", SetDefaultPosition);
    }

    void OnDisable()
    {
        EventManager.StopListening("EndGame",SetDefaultPosition);
    }
}
