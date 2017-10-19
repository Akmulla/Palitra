using UnityEngine;

public class ButtonStart : MonoBehaviour
{
	public void BeginGame()
    {
        if (!Hearts.h.CheckHearts())
            return;

        SoundManager.soundManager.SingleSound(SoundSample.Start);
        GameController.gameController.BeginGame();
    }
}
