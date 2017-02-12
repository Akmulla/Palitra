﻿using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour
{
    SpriteRenderer sprite_rend;
    float height;
    Transform tran;
    bool active;

    void Awake()
    {
        sprite_rend = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        tran = GetComponent<Transform>();
        height = sprite_rend.sprite.bounds.extents.y;
    }

    void OnEnable()
    {
        active = true;
        Color[] colors = GameController.game_controller.GetLvlData().colors;
        sprite_rend.color = colors[Random.Range(0, colors.Length)];
    }

    void Update()
    {
        if ((active)&&(tran.position.y-height < Ball.ball.tran.position.y))
        {
            Ball.ball.LinePassed(sprite_rend.color);
            active = false;
        }
    }



    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Ball"))
    //    {
    //        //if ((sprite_rend.color != other.GetComponent<SpriteRenderer>().color)&&(active))
    //        if (sprite_rend.color != other.GetComponent<SpriteRenderer>().color)
    //        {
    //            //active = false;
    //            GameController.game_controller.GameOver();
    //        }
    //    }
    //}
}