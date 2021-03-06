﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAway : MonoBehaviour
{
    public bool right;
    public bool by_position;
    bool rolling;
    float move_speed=10.0f;
    float rotate_speed=-120.0f;
    Vector3 dir_right=new Vector3(1.0f,1.0f,0.0f);
    Vector3 dir_left = new Vector3(-1.0f, 1.0f, 0.0f);

    Quaternion saved_rot;
    bool saved = false;

    public void Roll()
    {
        if (by_position)
        {
            if (transform.position.x >= 0.0f)
            {
                right = true;
            }
            else
            {
                right = false;
            }
        }
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
            transform.Rotate(new Vector3(0.0f, 0.0f, rotate_speed * Time.deltaTime));
        }
        else
        {
            transform.position += dir_left.normalized * move_speed * Time.deltaTime;
            transform.Rotate(new Vector3(0.0f, 0.0f, -rotate_speed * Time.deltaTime));
        }
       // transform.Rotate(new Vector3(0.0f,0.0f,rotate_speed*Time.deltaTime));
    }

    void OnEnable()
    {
        if (!saved)
        {
            saved_rot = transform.rotation;
            saved = true;
        }
        rolling = false;
        transform.rotation=saved_rot;
    }
}
