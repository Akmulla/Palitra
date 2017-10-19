using UnityEngine;
using UnityEngine.UI;

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

        if (GameController.gameController.GetState() != GameState.GameOver)
            return;

        GameController.gameController.BeginGame();
        button.interactable = false;
    }

   
}
