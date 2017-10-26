using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAway : MonoBehaviour
{
    public bool right;
    bool rolling;
    float move_speed=3.0f;
    float rotate_speed=60.0f;
    Vector3 dir_right=new Vector3(1.0f,1.0f,0.0f);
    Vector3 dir_left = new Vector3(-1.0f, 1.0f, 0.0f);

    public void Roll()
    {
        rolling = true;
    }

    void Update()
    {
        if (!rolling)
            return;

        //transform.position += Vector3.right * move_speed * Time.deltaTime;
        if (right)
        {
            transform.position += dir_right.normalized * move_speed * Time.deltaTime;
        }
        else
        {
            transform.position += dir_left.normalized * move_speed * Time.deltaTime;
        }
        transform.Rotate(new Vector3(0.0f,0.0f,rotate_speed*Time.deltaTime));
    }

    void OnEnable()
    {
        rolling = false;
    }
}
