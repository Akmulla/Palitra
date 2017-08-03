using UnityEngine;
using System.Collections;

public class Continue : MonoBehaviour
{
    public void ContinueGame()
    {
        if (GameController.game_controller.GetState() == GameState.Pause)
            GameController.game_controller.Continue();
        else
            GameController.game_controller.BeginGame();
    }
}
