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
        if (!Hearts.h.CheckHearts())
            return;

        if (GameController.game_controller.GetState() != GameState.GameOver)
            return;

        GameController.game_controller.BeginGame();
        button.interactable = false;
    }

   
}
