using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Continue : MonoBehaviour
{
    public Button button;
    public PauseStartAnim anim;
    [SerializeField] AAT_Ads aatAds;

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

        aatAds.ShowFullscreen();
        GameController.game_controller.BeginGame();
        button.interactable = false;
        
    }


   
}
