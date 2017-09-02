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
        EventManager.StartListening("LvlFinished", LvlChanged);
    }

    void OnDisable()
    {
        EventManager.StopListening("LvlFinished", LvlChanged);
    }

    void LvlChanged()
    {
        text.text =( GameController.game_controller.GetCurrentLvl()+1).ToString();
    }
}
