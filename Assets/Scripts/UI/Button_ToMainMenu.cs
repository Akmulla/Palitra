using UnityEngine;

public class ButtonToMainMenu : MonoBehaviour
{
    public void ToMainMenu()
    {
        SoundManager.soundManager.SingleSound(SoundSample.ToMain);
        GameController.gameController.ToMainMenu();
    }
}
