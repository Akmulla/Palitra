using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Continue : MonoBehaviour
{
    public Button button;
    public PauseStartAnim anim;

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
        anim.Animate();


        GameController.game_controller.BeginGame();
        button.interactable = false;
        
    }

   
}
