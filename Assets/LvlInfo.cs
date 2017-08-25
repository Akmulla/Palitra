using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlInfo : MonoBehaviour
{
    public Text text;
    int lines_passed;

    void Start()
    {
        text = GetComponent<Text>();
        text.text = GameController.game_controller.GetCurrentLvl().ToString();
    }

    void OnEnable()
    {
        EventManager.StartListening("ChangeLvl", LvlChanged);
    }

    void OnDisable()
    {
        EventManager.StopListening("ChangeLvl", LvlChanged);
    }

    void LvlChanged()
    {
        text.text = GameController.game_controller.GetCurrentLvl().ToString();
    }
}
