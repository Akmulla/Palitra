using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAway : MonoBehaviour
{
    public bool right;
    bool rolling;
    public float move_speed;
    public float rotate_speed;

    public void Roll()
    {
        rolling = true;
    }

    void Update()
    {
        if (!rolling)
            return;

        transform.position += Vector3.right * move_speed * Time.deltaTime;
        transform.Rotate(new Vector3(0.0f,0.0f,60.0f*Time.deltaTime));
    }

    void OnEnable()
    {
        rolling = false;
    }
}
