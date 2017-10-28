using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    public static Hearts h;
    public TimeManager time_manager;
    int hearts;
    public Text text;
    public Text text2;
    public Text heartMenuText;



    void Awake()
    {
        h = this;
        
    }

    void Start()
    {
        hearts=SaveLoadGame.save_load.LoadHearts();

        text.text = hearts.ToString();
        text2.text= hearts.ToString();
        heartMenuText.text = hearts.ToString();
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
            heartMenuText.text = hearts.ToString();
            text2.text = hearts.ToString();
            if (hearts <= 10)
            {
                //timeObj.SetActive();
                time_manager.EnableTimer();
            }
            else
            {
                time_manager.DisableTimer();
            }

            SaveLoadGame.save_load.SaveHearts();
        }
    }

    public bool CheckHearts()
    {
        return hearts>0;
    }

    void OnEnable()
    {
        EventManager.StartListening("BeginGame", BeginLvl);
        text.text = hearts.ToString();
        text2.text = hearts.ToString();
        heartMenuText.text = hearts.ToString();
    }

    void OnDisable()
    {
        EventManager.StopListening("BeginGame", BeginLvl);
    }

    void BeginLvl()
    {
        //Heart--;
        
    }
}
