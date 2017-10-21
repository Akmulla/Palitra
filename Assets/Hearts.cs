using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    public static Hearts h;
    int hearts;
    public Text text;
    public Text heartMenuText;

    void Awake()
    {
        h = this;
        
    }

    void Start()
    {
        hearts=SaveLoadGame.save_load.LoadHearts();
        text.text = hearts.ToString();
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
            heartMenuText.text= hearts.ToString();
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
