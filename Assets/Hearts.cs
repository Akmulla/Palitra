﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    public static Hearts h;
    [SerializeField]
    int hearts = 10;
    public Text text;
   

    void Awake()
    {
        h = this;
        text.text = hearts.ToString();
    }

    public int Heart
    {
        get
        {
            return hearts;
        }
        set
        {
            hearts = value;
            text.text = hearts.ToString();
        }
    }

    public bool CheckHearts()
    {
        if (hearts>0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnEnable()
    {
        EventManager.StartListening("BeginGame", BeginLvl);
    }

    void OnDisable()
    {
        EventManager.StopListening("BeginGame", BeginLvl);
    }

    void BeginLvl()
    {
        Heart--;
    }
}
