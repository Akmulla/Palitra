using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Continue : MonoBehaviour
{
    public Button button;

    void OnEnable()
    {
        button.interactable = true;
    }

    public void ContinueGame()
    {
        if (GameController.game_controller.GetState() == GameState.GameOver)
        {
            button.interactable = false;
            GameController.game_controller.BeginGame();
        }
            
    }
}
