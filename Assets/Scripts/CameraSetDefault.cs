using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetDefault : MonoBehaviour
{
    Transform tran;
    Vector3 defaultPosition = new Vector3(0.0f, 0.0f, -10.0f);

    void Start()
    {
        tran = GetComponent<Transform>();
    }

    void SetDefaultPosition()
    {
        tran.position = defaultPosition;
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
