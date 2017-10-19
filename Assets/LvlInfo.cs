using UnityEngine;
using UnityEngine.UI;

public class LvlInfo : MonoBehaviour
{
    public Text text;
    int _linesPassed;

    void Start()
    {
        text = GetComponent<Text>();
        // text.text = GameController.game_controller.GetCurrentLvl().ToString();
        text.text = GameController.gameController.CurrentLvl.ToString();
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
        text.text =( GameController.gameController.CurrentLvl+1).ToString();
    }
}
