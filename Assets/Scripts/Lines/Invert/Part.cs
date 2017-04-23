﻿using UnityEngine;
using System.Collections;

public class Part : MonoBehaviour
{
    SpriteRenderer sprite_rend;

    void Awake()
    {
        sprite_rend = GetComponent<SpriteRenderer>();
    }

    public Color GetColor()
    {
        if (sprite_rend==null)
        {
            sprite_rend = GetComponent<SpriteRenderer>();
        }
        return sprite_rend.color;
    }

    public void SetRandomColor()
    {
        if (sprite_rend != null)
        {
            sprite_rend.color = GameController.game_controller.GetLvlData().colors
                [Random.Range(0, GameController.game_controller.GetLvlData().colors.Length)];
        }
    }

    public void SetColor(Color color)
    {
        if (sprite_rend == null)
        {
            sprite_rend = GetComponent<SpriteRenderer>();
        }
        if (sprite_rend != null)
        {
            sprite_rend.color = color;
        }
    }

    public void SetGreyColor()
    {
        if (sprite_rend == null)
        {
            sprite_rend = GetComponent<SpriteRenderer>();
        }
        if (sprite_rend != null)
        {
            sprite_rend.color = Color.gray;
        }
    }
}